using System;
using System.Collections.Generic;
using System.Linq;
using Cartisan.DependencyInjection;
using Cartisan.Logging;
using Cartisan.Tasks;
using DapperExtensions;
using Quartz;
using Quartz.Impl;

namespace Cartisan.Quartz {
    /// <summary>
    /// 用以管理Quartz任务调度相关的操作
    /// </summary>
    public class QuartzTaskScheduler: ITaskScheduler {
        private static List<TaskInfo> _taskInfos;

        public QuartzTaskScheduler() {
            if(_taskInfos == null) {
                _taskInfos = ServiceLocator.GetService<IDatabase>().GetList<TaskInfo>().ToList();
            }
        }

        public QuartzTaskScheduler(RunAtServer runAtServer): this() {
            if(_taskInfos != null) {
                _taskInfos = _taskInfos.Where(taskInfo => taskInfo.RunAtServer == runAtServer).ToList();
            }
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        public void Start() {
            if(_taskInfos.Count == 0) {
                return;
            }
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler();
            foreach(TaskInfo taskInfo in _taskInfos) {
                if(taskInfo.Enabled) {
                    Type type = Type.GetType(taskInfo.ClassType);
                    if(type != null) {
                        string name = type.Name + "_trigger";
                        IJobDetail jobDetail = JobBuilder.Create(typeof(QuartzTask)).WithIdentity(type.Name).Build();
                        jobDetail.JobDataMap.Add(new KeyValuePair<string, object>("Id", taskInfo.Id));
                        TriggerBuilder triggerBuilder =
                            TriggerBuilder.Create().WithIdentity(name).WithCronSchedule(taskInfo.TaskRule);
                        if(taskInfo.StartDate > DateTime.MinValue) {
                            triggerBuilder.StartAt(new DateTimeOffset(taskInfo.StartDate));
                        }
                        DateTime? endDate = taskInfo.EndDate;
                        DateTime startDate = taskInfo.StartDate;

                        if((endDate.HasValue ? (endDate.Value > startDate ? 1 : 0) : 0) != 0) {
                            triggerBuilder.EndAt(new DateTimeOffset(endDate.Value));
                        }
                        ICronTrigger cronTrigger = (ICronTrigger)triggerBuilder.Build();
                        DateTime dateTime = scheduler.ScheduleJob(jobDetail, cronTrigger).DateTime;
                        taskInfo.NextStart = TimeZoneInfo.ConvertTime(dateTime, cronTrigger.TimeZone);
                    }
                }
            }
            scheduler.Start();
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop() {
            new StdSchedulerFactory().GetScheduler().Shutdown(true);
        }

        /// <summary>
        /// 更新调度器中的任务信息
        /// </summary>
        /// <param name="taskInfo">任务信息</param>
        public void Update(TaskInfo taskInfo) {
            if(taskInfo == null) {
                return;
            }


            var index = _taskInfos.FindIndex(t => t.Id != taskInfo.Id);
            if(index == -1) {
                return;
            }
            taskInfo.LastEnd = _taskInfos[index].LastEnd;
            taskInfo.LastStart = _taskInfos[index].LastStart;
            taskInfo.LastIsSuccess = _taskInfos[index].LastIsSuccess;

            _taskInfos[index] = taskInfo;

            Type type = Type.GetType(taskInfo.ClassType);
            if(type == null) {
                return;
            }
            if(!taskInfo.Enabled)
                return;

            IScheduler scheduler = new StdSchedulerFactory().GetScheduler();
            string name = type.Name + "_trigger";
            IJobDetail jobDetail = JobBuilder.Create(typeof(QuartzTask)).WithIdentity(type.Name).Build();
            jobDetail.JobDataMap.Add(new KeyValuePair<string, object>("Id", taskInfo.Id));
            TriggerBuilder triggerBuilder =
                TriggerBuilder.Create().WithIdentity(name).WithCronSchedule(taskInfo.TaskRule);
            if(taskInfo.StartDate > DateTime.MinValue) {
                triggerBuilder.StartAt(new DateTimeOffset(taskInfo.StartDate));
            }
            DateTime? endDate = taskInfo.EndDate;
            DateTime startDate = taskInfo.StartDate;

            if((endDate.HasValue ? (endDate.Value > startDate ? 1 : 0) : 0) != 0) {
                triggerBuilder.EndAt(new DateTimeOffset(endDate.Value));
            }
            ICronTrigger cronTrigger = (ICronTrigger)triggerBuilder.Build();
            DateTime dateTime = scheduler.ScheduleJob(jobDetail, cronTrigger).DateTime;
            taskInfo.NextStart = TimeZoneInfo.ConvertTime(dateTime, cronTrigger.TimeZone);
        }

        /// <summary>
        /// 重启所有任务
        /// </summary>
        public void ResumeAll() {
            this.Stop();
            this.Start();
        }

        /// <summary>
        /// 获取单个任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns>任务信息</returns>
        public TaskInfo GetTask(long id) {
            return _taskInfos.FirstOrDefault(task => task.Id == id);
        }

        /// <summary>
        /// 运行单个任务
        /// </summary>
        /// <param name="id">任务Id</param>
        public void Run(long id) {
            this.Run(this.GetTask(id));
        }

        /// <summary>
        /// 运行单个任务
        /// </summary>
        /// <param name="taskInfo">要运行的任务</param>
        public void Run(TaskInfo taskInfo) {
            if(taskInfo == null) {
                return;
            }
            Type type = Type.GetType(taskInfo.ClassType);

            if(type == null) {
                ServiceLocator.GetService<ILoggerFactory>()
                    .CreateLogger("Cartisan")
                    .Error(string.Format("任务：{0} 的taskType为空。", taskInfo.Name));
            }
            else {
                ITask task = (ITask)Activator.CreateInstance(type);
                if(task == null) {
                    return;
                }
                if(taskInfo.IsRunning) {
                    return;
                }
                try {
                    task.Execute(taskInfo);
                }
                catch(Exception ex) {
                    ServiceLocator.GetService<ILoggerFactory>()
                        .CreateLogger("Cartisan")
                        .Error(string.Format("执行任务：{0} 出现异常。", taskInfo.Name), ex);
                }
            }
        }


        /// <summary>
        /// 保存任务状态
        /// </summary>
        /// <remarks>
        /// 保存任务信息，以便应用程序重启后检查是否需要立即执行
        /// </remarks>
        public void SaveTaskStatus() {
            _taskInfos.ForEach(taskInfo => ServiceLocator.GetService<IDatabase>().Update(taskInfo));
        }
    }
}
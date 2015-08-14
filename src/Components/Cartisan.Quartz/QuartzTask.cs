using System;
using System.Data;
using Cartisan.DependencyInjection;
using Cartisan.Logging;
using Cartisan.Tasks;
using Dapper;
using Quartz;

namespace Cartisan.Quartz {
    /// <summary>
    /// Quartz 任务实现
    /// </summary>
    public class QuartzTask : IJob {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context">Quartz任务运行环境</param>
        /// <remarks>
        /// 外部不需要调用，仅用于任务调度组建内部
        /// </remarks>
        public void Execute(IJobExecutionContext context) {
            TaskInfo task = ServiceLocator.GetService<ITaskScheduler>().GetTask(context.JobDetail.JobDataMap.GetInt("Id"));
            AssertionConcern.NotNull(task, "没有找到任务。");
            TaskService taskService = new TaskService();
            task.IsRunning = true;
            task.LastStart = DateTime.Now;
            try {
                ((ITask)Activator.CreateInstance(Type.GetType(task.ClassType))).Execute(task);
                task.LastIsSuccess = true;
            }
            catch (Exception ex) {
                var logger = ServiceLocator.GetService<ILoggerFactory>().CreateLogger("Cartisan");
                logger.Error(string.Format("类型为 {0} 的任务 {1} 运行时发生异常。",
                    context.JobDetail.JobType, context.JobDetail.Key), ex);
                task.IsRunning = false;
            }
            task.IsRunning = false;
            if (context.NextFireTimeUtc.HasValue) {
                task.NextStart = context.NextFireTimeUtc.Value.UtcDateTime;
            }
            task.LastEnd = DateTime.Now;
        }
    }
}
namespace Cartisan.Tasks {
    /// <summary>
    /// 任务执行控制器
    /// </summary>
    /// <remarks>需要从DI容器中获取注册的任务</remarks>
    public interface ITaskScheduler {
        /// <summary>
        /// 开始执行任务
        /// </summary>
        void Start();

        /// <summary>
        /// 停止任务
        /// </summary>
        void Stop();

        /// <summary>
        /// 更新调度器中的任务信息
        /// </summary>
        /// <param name="taskInfo">任务信息</param>
        void Update(TaskInfo taskInfo);

        /// <summary>
        /// 重启所有任务
        /// </summary>
        void ResumeAll();

        /// <summary>
        /// 获取单个任务
        /// </summary>
        /// <param name="id">任务Id</param>
        /// <returns>任务信息</returns>
        TaskInfo GetTask(long id);

        /// <summary>
        /// 执行单个任务
        /// </summary>
        /// <param name="id">任务Id</param>
        void Run(long id);

        /// <summary>
        /// 保存任务状态
        /// </summary>
        /// <remarks>
        /// 将当前需要 ResumeContinue 为 true 的任务记录，以便应用程序重启后检查是否需要立即执行
        /// </remarks>
        void SaveTaskStatus();
    }
}
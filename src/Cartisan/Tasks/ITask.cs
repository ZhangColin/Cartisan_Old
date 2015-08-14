namespace Cartisan.Tasks {
    /// <summary>
    /// 任务接口，所有任务都需要实现此接口
    /// </summary>
    public interface ITask {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task">任务信息</param>
        void Execute(TaskInfo task = null);
    }
}
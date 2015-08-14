namespace Cartisan.Events {
    /// <summary>
    /// 事件发布者
    /// </summary>
    public interface IEventPublisher {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="events"></param>
        void Publish(params IEvent[] events);
    }
}
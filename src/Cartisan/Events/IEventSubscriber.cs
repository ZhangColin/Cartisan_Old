namespace Cartisan.Events {
    /// <summary>
    /// 事件订阅
    /// </summary>
    public interface IEventSubscriber<in TEvent> where TEvent: class , IEvent {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="event"></param>
        void Handle(TEvent @event);
    }
}
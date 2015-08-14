using System.Collections.Generic;

namespace Cartisan.Events {
    /// <summary>
    /// 事件订阅者提供器
    /// </summary>
    public interface IEventSubscriberProvider {
        /// <summary>
        /// 获取事件的所有订阅者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        IEnumerable<IEventSubscriber<T>> GetHandlers<T>(T @event) where T : class, IEvent;
    }
}
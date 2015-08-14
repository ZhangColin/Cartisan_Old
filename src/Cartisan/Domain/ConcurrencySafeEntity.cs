using System;

namespace Cartisan.Domain {
    /// <summary>
    /// 并发安全的实体
    /// </summary>
    public abstract class ConcurrencySafeEntity : Entity {
        public virtual int ConcurrencyVersion { get; protected set; }

        public virtual void FailWhenConcurrencyVersion(int version) {
            if (version != ConcurrencyVersion) {
                throw new InvalidOperationException("并发检查：实体数据已经修改。");
            }
        }
    }
}
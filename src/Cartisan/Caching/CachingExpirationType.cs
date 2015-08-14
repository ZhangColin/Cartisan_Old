namespace Cartisan.Caching {
    /// <summary>
    /// 缓存期限类型
    /// </summary>
    public enum CachingExpirationType {
        /// <summary>
        /// 永久不变的(默认：1天乘时间因子)
        /// </summary>
        Invariable,
        /// <summary>
        /// 稳定数据对象集合(默认：8小时乘时间因子)
        /// </summary>
        Stable,
        /// <summary>
        /// 相对稳定对象集合(默认：2小时乘时间因子)
        /// </summary>
        RelativelyStable,
        /// <summary>
        /// 常用的单个对象对象集合(默认：10分钟乘时间因子)
        /// </summary>
        UsualSingleObject,
        /// <summary>
        /// 常用的对象集合对象集合(默认：5分钟乘时间因子)
        /// </summary>
        UsualObjectCollection,
        /// <summary>
        /// 单个对象对象集合(默认：3分钟乘时间因子)
        /// </summary>
        SingleObject,
        /// <summary>
        /// 对象集合(默认：3分钟乘时间因子)
        /// </summary>
        ObjectCollection
    }
}
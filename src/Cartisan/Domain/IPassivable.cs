namespace Cartisan.Domain {
    /// <summary>
    /// 用于标识实体启用/不启用
    /// </summary>
    public interface IPassivable {
        /// <summary>
        /// True: 启用实体
        /// False: 不启用实体
        /// </summary>
        bool IsActive { get; set; }
    }
}
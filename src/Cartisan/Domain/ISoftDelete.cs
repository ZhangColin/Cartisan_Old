namespace Cartisan.Domain {
    /// <summary>
    /// 用于规范实体软删除。实体软删除是没有真正删除,数据库中标记为IsDeleted = true,但应用程序不能检索到。
    /// </summary>
    public interface ISoftDelete {
        /// <summary>
        /// 用于标记实体软删除。
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
namespace Cartisan.Specification {
    /// <summary>
    /// 规则解析器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecificationParser<T> {
        T Parse<TEntity>(ISpecification<TEntity> specification);
    }
}
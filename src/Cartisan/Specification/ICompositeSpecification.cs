namespace Cartisan.Specification {
    /// <summary>
    /// 规则组合接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICompositeSpecification<T>: ISpecification<T> {
        ISpecification<T> Left { get; }
        ISpecification<T> Right { get; }
    }
}
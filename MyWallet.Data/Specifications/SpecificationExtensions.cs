namespace MyWallet.Data.Specifications
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> target, ISpecification<T> specification) => new AndSpecification<T>(target, specification);
    }
}

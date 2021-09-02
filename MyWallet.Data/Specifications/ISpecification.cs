using System;
using System.Linq.Expressions;

namespace MyWallet.Data.Specifications
{
    public interface ISpecification<T>
    {
        Func<T, bool> IsSatisfiedBy { get; }

        Expression<Func<T, bool>> SpecExpression { get; }
    }
}

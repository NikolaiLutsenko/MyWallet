using System;
using System.Linq.Expressions;

namespace MyWallet.Data.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        private Func<T, bool> _predicate;

        public Func<T, bool> IsSatisfiedBy => _predicate ??= SpecExpression.Compile();

        public abstract Expression<Func<T, bool>> SpecExpression { get; }
    }
}

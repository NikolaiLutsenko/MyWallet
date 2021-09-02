using MyWallet.Data.Interfaces;
using System;
using System.Linq.Expressions;

namespace MyWallet.Data.Specifications
{
    public class CategorySpecification<T> : BaseSpecification<T> where T : HasCategory
    {
        private readonly Guid? _categoryId;

        public CategorySpecification(Guid? categoryId)
        {
            _categoryId = categoryId;
        }

        public override Expression<Func<T, bool>> SpecExpression =>
            (o) => !_categoryId.HasValue || o.CategoryId == _categoryId.Value || o.Category.Parrent.Id == _categoryId.Value;
    }
}

using MyWallet.Data.Interfaces;
using MyWallet.Data.ValueObjects;
using System;
using System.Linq.Expressions;

namespace MyWallet.Data.Specifications
{
    public class DateRangeSpecification<T> : BaseSpecification<T> where T : HasCreatedAt
    {
        private readonly DateRange _dateRange;

        public DateRangeSpecification(DateRange dateRange)
        {
            _dateRange = dateRange;
        }

        public override Expression<Func<T, bool>> SpecExpression => 
            (o) => _dateRange.IsEmpty() ||
                ((!_dateRange.HasFrom || o.Date.Date >= _dateRange.From)
                 && (!_dateRange.HasTo || o.Date.Date <= _dateRange.To));
    }
}

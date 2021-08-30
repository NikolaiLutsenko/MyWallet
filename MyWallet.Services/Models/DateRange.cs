using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MyWallet.Models
{
    [DebuggerDisplay("From = {From}, To = {To}")]
    public struct DateRange
    {
        private readonly DateTime? _from;
        private readonly DateTime? _to;

        public DateRange(DateTime? from, DateTime? to)
        {
            if (from == default) throw new ArgumentException(nameof(from));
            if (to == default) throw new ArgumentException(nameof(to));

            if (from > to) throw new ArgumentException("from cannot be greater than to");

            _from = from;
            _to = to;
        }

        internal IEnumerable<object> GetFields()
        {
            yield return From;
            yield return To;
        }

        public DateTime From => _from.Value;

        public DateTime To => _to.Value;

        public bool HasFrom => _from.HasValue;

        public bool HasTo => _to.HasValue;

        public static DateRange Day => new(DateTime.Now.Date, DateTime.Now.Date);

        public static DateRange PrevDay => new(Day.From.AddDays(-1), Day.From.AddDays(-1));

        public static DateRange Week => new(DateTime.Now.Date.AddDays(-ToNormalDayOfWeek(DateTime.Now.Date.DayOfWeek)+1), DateTime.Now.Date);

        public static DateRange PrevWeek => new(Week.From.AddDays(-7), Week.From.AddDays(-1));

        public static DateRange Month => new(DateTime.Now.Date.AddDays(-DateTime.Now.Day + 1), DateTime.Now.Date);

        public static DateRange PrevMonth => new(Month.From.AddMonths(-1), Month.From.AddDays(-1));

        private static int ToNormalDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Sunday => 7,
                _ => (int)dayOfWeek
            };
        }

        public static bool operator ==(DateRange left, DateRange right) => left.Equals(right);

        public static bool operator !=(DateRange left, DateRange right) => !left.Equals(right);

        public override bool Equals(object obj)
        {
            if (obj is not DateRange targetObj)
                return false;

            return GetFields().SequenceEqual(targetObj.GetFields());
        }

        public override int GetHashCode() => string.Join("|", GetFields().Select(x => x.ToString())).GetHashCode();
    }
}

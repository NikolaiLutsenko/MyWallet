using System;
using System.Diagnostics;

namespace MyWallet.Models
{
    [DebuggerDisplay("From = {From}, To = {To}")]
    public struct DateRange
    {
        private readonly DateTime _from;
        private readonly DateTime _to;

        public DateRange(DateTime from, DateTime to)
        {
            if (from == default) throw new ArgumentException(nameof(from));
            if (to == default) throw new ArgumentException(nameof(to));

            if (from > to) throw new ArgumentException("from cannot be greater than to");

            _from = from;
            _to = to;
        }

        public DateTime From => _from;

        public DateTime To => _to;

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
    }
}

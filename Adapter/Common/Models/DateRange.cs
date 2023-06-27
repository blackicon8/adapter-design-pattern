using System.Collections;

namespace Adapter.Common.Models
{
    public struct DateRange : IEquatable<DateRange>, IEnumerable<DateTime>
    {
        public DateRange(string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(startDate))
                throw new ArgumentNullException(nameof(startDate),
                    "Start date is null or empty");

            if (string.IsNullOrWhiteSpace(endDate))
                throw new ArgumentNullException(nameof(endDate),
                    "End date is null or empty");

            if (!DateTime.TryParse(startDate, out var startDateResult))
                throw new FormatException("Incorrect start date format");

            if (!DateTime.TryParse(endDate, out var endDateResult))
                throw new FormatException("Incorrect end date format");

            if (endDateResult < startDateResult)
                throw new ArgumentOutOfRangeException(nameof(endDate),
                    "End date is less than start date");

            StartDate = startDateResult;
            EndDate = endDateResult;
        }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentOutOfRangeException(nameof(endDate),
                    "End date is less than start date");

            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public TimeSpan Interval
        {
            get { return EndDate - StartDate; }
        }

        public IList<DateTime> Dates
        {
            get
            {
                var startDate = StartDate;
                return Enumerable.Range(0, Interval.Days + 1)
                    .Select(offset => startDate.AddDays(offset))
                    .ToList();
            }
        }

        public bool Contains(DateTime date)
        {
            if (StartDate.Ticks <= date.Ticks && date.Ticks <= EndDate.Ticks)
                return true;

            return false;
        }

        public static IEnumerable<DateRange> Split(DateRange dateRange, int chunkSize)
        {
            if (chunkSize < 1)
                throw new ArgumentException(nameof(chunkSize),
                    "Chunk size is less than 1");

            var result = new List<DateRange>();
            var chunkStart = dateRange.StartDate;

            while (chunkStart <= dateRange.EndDate)
            {
                var chunkEnd = chunkStart.AddDays(chunkSize - 1);
                chunkEnd = chunkEnd <= dateRange.EndDate ? chunkEnd : dateRange.EndDate;

                result.Add(new DateRange(chunkStart, chunkEnd));

                chunkStart = chunkEnd.AddDays(1);
            }

            return result;
        }

        public static IEnumerable<DateRange> Split(DateRange dateRange, DayOfWeek chunkSeparator)
        {
            var result = new List<DateRange>();

            var startDate = dateRange.StartDate;
            var chunkStart = startDate;

            while (chunkStart <= dateRange.EndDate)
            {
                var chunkEnd = chunkStart.AddDays(1);

                if (chunkEnd > dateRange.EndDate || chunkEnd.DayOfWeek == chunkSeparator)
                {
                    result.Add(new DateRange(startDate, chunkEnd.AddDays(-1)));
                    startDate = chunkEnd;
                }

                chunkStart = chunkEnd;
            }

            return result;
        }

        public bool Equals(DateRange other)
        {
            return ((StartDate == other.StartDate) && (EndDate == other.EndDate));
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            foreach (var date in Dates)
            {
                yield return date;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using System;

namespace GuildfordBoroughCouncil.Linq
{
    public static partial class DateTimeExtensions
    {
        public static string ToString(this DateTime? source, string format)
        {
            return source.ToString(format, "");
        }

        public static string ToString(this DateTime? source, string format, string NullValue = "")
        {
            if (source.HasValue)
                return source.Value.ToString(format);
            else
                return NullValue;
        }

        /// <summary>
        /// This method will return the next collection date for a given date
        /// </summary>
        /// <param name="FirstCollection">First collection date</param>
        /// <returns>Returns the next collection date.</returns>
        public static DateTime MakeCurrent(this DateTime source, byte multiplier = 1)
        {
            var NewDate = source;
            if (NewDate < DateTime.Today)
            {
                do
                {
                    NewDate = NewDate.AddDays(7 * multiplier);
                }
                while (NewDate < DateTime.Today);
            }

            return NewDate;
        }

        /// <summary>
        /// Returns true if the next collection is a fortnight
        /// </summary>
        /// <param name="source">Date 1</param>
        /// <param name="ComparisonDate">Date 2</param>
        /// <returns>Returns if the next collection date is a fortnight (or multiple thereof) from the first collection date (<c>true</c>/<c>false</c>).</returns>
        public static Boolean IsFortnight(this DateTime source, DateTime ComparisonDate)
        {
            try
            {
                TimeSpan difference = source - ComparisonDate;
                int remainder = difference.Days % 14;
                return remainder.Equals(0);
            }
            catch
            {
                return false;
            }
        }

        public static DateTime NearestQuarterHour(this DateTime source)
        {
            var Mins = source.Minute;
            int Upper = 15;
            bool Found = false;

            while (!Found)
            {
                if (Mins < (Upper - 10))
                {
                    Mins = Upper - 15;
                    Found = true;
                }
                else if (Mins < Upper)
                {
                    Mins = Upper;
                    Found = true;
                }

                Upper = Upper + 15;
            }

            return source.AddMinutes((source.Minute * -1) + Mins);
        }
    }
}
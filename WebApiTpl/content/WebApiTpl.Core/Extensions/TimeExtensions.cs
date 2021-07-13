using System;

namespace WebApiTpl.Core.Extensions
{
    public class TimeExtensions
    {
        /// <summary>
        ///     时间转int
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int TimeToInt(DateTime time)
        {
            var Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int) (time.AddHours(-8) - Jan1st1970).TotalSeconds;
        }

        /// <summary>
        ///     int转时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime IntToTime(int time)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(time).AddHours(8);
        }
    }
}
using System;

namespace FootballManagerEF
{
    public static class Utils
    {
        public static string ChangeDateFormatToYYYYMMDD(string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;

            return DateTime.Parse(date).ToString("yyyy-MM-dd");
        }

        public static bool IsOlderThanAMonth(string date)
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);

            int result = DateTime.Compare(DateTime.Parse(date), lastMonth);

            if (result < 0)
                return true;

            return false;
        }

        public static DateTime ThreeWeeksAgo()
        {
            return DateTime.Now.AddDays(-21);
        }
    }
}

using System;

namespace FootballManagerEF.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal Round(this decimal value)
        {
            return Math.Round(value, 0, MidpointRounding.AwayFromZero);
        }
    }
}

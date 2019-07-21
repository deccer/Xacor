using System;

namespace Xacor.Game.ECS
{
    public static class FastDateTime
    {
        private static readonly TimeSpan LocalUtcOffset = TimeZoneInfo.Utc.GetUtcOffset(DateTime.Now);

        public static DateTime Now => DateTime.UtcNow + LocalUtcOffset;

        public static double GetMicroSeconds(long ticks)
        {
            return ticks * 0.1;
        }

        public static double GetNanoSeconds(long ticks)
        {
            return ticks * 100.0;
        }

        public static string ToString(TimeSpan timeSpan)
        {
            return $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds * 0.1:00}";
        }

        public new static string ToString()
        {
            return Now.ToString("HH:mm:ss.ffffff");
        }
    }
}
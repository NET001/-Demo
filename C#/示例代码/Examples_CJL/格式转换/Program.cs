using System;

namespace 格式转换
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConvertDateTimeToInt(DateTime.Now));
            Console.ReadKey();
        }
        public static long ConvertDateTimeToInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
    }
}

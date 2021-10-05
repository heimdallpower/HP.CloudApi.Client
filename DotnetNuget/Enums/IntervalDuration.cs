namespace HeimdallApi.Enums
{
    // See https://www.345tool.com/time-util/time-duration-calculator
    public class IntervalDuration
    {
        public static string Every5Minutes => "PT5M";
        public static string EveryHour => "PT1H";
        public static string Every90Mins => "PT1H30M";
        public static string EveryDay=> "P1D";
        public static string EveryWeek=> "P7D";

    }
}

using System.Text.RegularExpressions;

namespace HeimdallPower.ExtensionMethods
{
    public static class StringExtensions
    {
        /// <summary>
        /// True if <paramref name="durationString"/> is a valid ISO 8601 duration.
        /// <see href="https://en.wikipedia.org/wiki/ISO_8601#Durations">https://en.wikipedia.org/wiki/ISO_8601#Durations</see> 
        /// </summary>
        public static bool IsValidIso8601Duration(this string durationString)
        {
            // https://stackoverflow.com/a/32045167/7188061
            var iso8601DurationPattern = @"^P(?!$)(\d+Y)?(\d+M)?(\d+W)?(\d+D)?(T(?=\d+[HMS])(\d+H)?(\d+M)?(\d+S)?)?$";
            return Regex.IsMatch(durationString, iso8601DurationPattern);
        }
    }
}

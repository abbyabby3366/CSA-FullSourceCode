using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace csa.Library
{
    public static class ExtensionMethod
    {
        public static string GetDescription(this Enum value)
        {
            return ((DescriptionAttribute)Attribute.GetCustomAttribute(
                value.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Single(x => x.GetValue(null).Equals(value)),
                typeof(DescriptionAttribute)))?.Description ?? value.ToString();
        }

        public static long GetUnixTimeStamp(this DateTime baseDateTime, int type = 0)
        {
            var dtOffset = new DateTimeOffset();
            dtOffset = DateTime.SpecifyKind(baseDateTime, DateTimeKind.Utc);

            if (type == 0)
            { return dtOffset.ToUnixTimeMilliseconds(); }
            else if (type == 1)
            { return dtOffset.ToUnixTimeSeconds(); }
            else
            { return dtOffset.ToUnixTimeMilliseconds(); }
        }

        public static DateTime GetGregorianDate(this DateTime baseDateTime)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en");
            System.Globalization.DateTimeFormatInfo dtf = culture.DateTimeFormat;
            dtf.Calendar = new System.Globalization.GregorianCalendar();
            dtf.FullDateTimePattern = "yyyy/MMM/dd HH:mm:ss:fff";
            dtf.ShortDatePattern = "yyyy/MMM/dd";
            dtf.ShortTimePattern = "HH:mm:ss:fff";
            dtf.MonthDayPattern = "MMM";

            if (!DateTime.TryParse(baseDateTime.ToString(dtf), out DateTime tmp))
            { tmp = baseDateTime; }

            DateTime dt = new DateTime(tmp.Year, tmp.Month, tmp.Day, baseDateTime.Hour, baseDateTime.Minute, baseDateTime.Second, baseDateTime.Millisecond, baseDateTime.Kind);

            //return DateTime.ParseExact(baseDateTime.ToString(dtf.FullDateTimePattern), "dd/MM/yyyy HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
            return DateTime.ParseExact(dt.ToString(dtf.FullDateTimePattern), "yyyy/MMM/dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        public static DateTime ToTimeZoneTime(this DateTime time, string timeZoneId = "Singapore Standard Time")
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return time.ToTimeZoneTime(tzi);
        }

        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="tzi"></param>
        /// <returns></returns>
        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }

        public static bool ContainText(this string thisObj, string value, StringComparison compareType)
        {
            return thisObj.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string EllipsString(this string inputString, int length)
        {
            return (string.IsNullOrEmpty(inputString) || inputString.Length <= length) ? inputString : string.Format("{0}...", inputString.Trim().Substring(0, Math.Min(length, inputString.Length)));
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str?.Trim());
        }

        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str?.Trim());
        }

        public static string ToLiteral(this string valueTextForCompiler)
        {
            return Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false);
        }

        public static long ToLong(this object v)
        {
            try
            {
                return Convert.ToInt64(v);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ToInt(this object v)
        {
            try
            {
                return Convert.ToInt32(v);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int? IfZeroToNull(this int v)
        {
            return v == 0 ? (int?)null : v;
        }

        public static long? IfZeroToNull(this long v)
        {
            return v == 0 ? (long?)null : v;
        }
    }
}
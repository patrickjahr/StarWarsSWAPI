using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Core.Utils.Extentions
{
    public static class ExtensionMethods
    {

        /// <summary>
        /// Adds the item range to the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="collection">The collection.</param>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Return a hexadecimal string of the given bytes.
        ///
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            //Fastest Method to Convert a Byte Array to an Hex String according to this comparison:
            // http://stackoverflow.com/a/624379/744806
            // http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa/14333437#14333437
            char[] c = new char[bytes.Length * 2];
            int b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }


        /// <summary>
        /// return 1.1.1970 0:00 in UTC Time
        /// </summary>
        /// <returns></returns>
        private static DateTime GetDateTimeJan1St1970()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Determines whether this instance is numeric.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool IsNumeric(this object obj)
        {
            var isNumeric = false;

            isNumeric |= obj is byte;
            isNumeric |= obj is sbyte;
            isNumeric |= obj is short;
            isNumeric |= obj is ushort;
            isNumeric |= obj is int;
            isNumeric |= obj is uint;
            isNumeric |= obj is long;
            isNumeric |= obj is ulong;
            isNumeric |= obj is decimal;
            isNumeric |= obj is double;
            isNumeric |= obj is float;

            return isNumeric;
        }

        /// <summary>
        /// Determines whether [is numeric string].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool IsNumericString(this object obj)
        {
            try
            {
                double val;
                return Double.TryParse(obj as String, out val);
            }
            catch (Exception e)
            {
                Debug.WriteLine("ExtensionMethods.cs | IsNumericString | " + e);
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is not null].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// Determines whether this instance is null.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Adds or update an object to the dictionary.
        /// </summary>
        /// <typeparam name="TK">The type of the k.</typeparam>
        /// <typeparam name="TV">The type of the v.</typeparam>
        /// <param name="dict">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddOrUpdate<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        /// <summary>
        /// Returns a new observable collection instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return new ObservableCollection<T>();
            return new ObservableCollection<T>(collection);
        }

        /// <summary>
        /// Determines whether this instance has items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static bool HasItems<T>(this IEnumerable<T> collection)
        {
            return collection.IsNotNull() && collection.Any();
        }

        /// <summary>
        /// Performs the action for each items in the enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration">The enumeration.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// converts local time to utc unixtime stamp
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static long ToUnixTimeStampMilliSeconds(this DateTime time)
        {
            time = time.AddSeconds(-1 * GetTimeOffsetInSecondsFromLocalTime(time));
            return (long)(time - GetDateTimeJan1St1970()).TotalMilliseconds;
        }

        /// <summary>
        /// Determines whether [is neither null nor empty].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static bool IsNeitherNullNorEmpty(this string input)
        {
            return !String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Determines whether [is no valid mail adress].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static bool IsNoValidMailAdress(this string input)
        {
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return !regex.IsMatch(input.ToLower());
        }


        /// <summary>
        /// converts utc unixtimestampt to local time
        /// </summary>
        /// <param name="milliSeconds">The milli seconds.</param>
        /// <returns></returns>
        public static DateTime FromUnixTimeStampMilliSeconds(this long milliSeconds)
        {
            DateTime date = GetDateTimeJan1St1970().AddMilliseconds(milliSeconds);
            date = date.AddSeconds(GetTimeOffsetInSecondsFromUtcTime(date));
            return date;
        }

        /// <summary>
        /// Gets the time offset in seconds from UTC time.
        /// </summary>
        /// <param name="computatedDatetime">The computated datetime.</param>
        /// <returns></returns>
        public static int GetTimeOffsetInSecondsFromUtcTime(DateTime computatedDatetime)
        {
            return (int)(computatedDatetime.ToLocalTime() - computatedDatetime).TotalSeconds;
        }

        /// <summary>
        /// Gets the time offset in seconds from local time.
        /// </summary>
        /// <param name="computatedDatetime">The computated datetime.</param>
        /// <returns></returns>
        public static int GetTimeOffsetInSecondsFromLocalTime(DateTime computatedDatetime)
        {
            return (int)(computatedDatetime - computatedDatetime.ToUniversalTime()).TotalSeconds;
        }

        /// <summary>
        /// Returns and removes the last item from the end of the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theList">The list.</param>
        /// <returns></returns>
        public static T Pop<T>(this IList<T> theList)
        {
            var local = theList[theList.Count - 1];
            theList.RemoveAt(theList.Count - 1);
            return local;
        }

        /// <summary>
        /// Adds the item to the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theList">The list.</param>
        /// <param name="item">The item.</param>
        public static void Push<T>(this IList<T> theList, T item)
        {
            theList.Add(item);
        }

        public static string GetIdFromUrl(this string url)
        {
            if (String.IsNullOrEmpty(url)) return String.Empty;
            var id = url.Substring(url.Length - 2, 1);
            return String.IsNullOrEmpty(id) ? String.Empty : id;
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static bool ContainsIgnoreCase(this string orginal, string checkValue)
        {
            return orginal.ToLower().Contains(checkValue.ToLower());
        }
    }
}
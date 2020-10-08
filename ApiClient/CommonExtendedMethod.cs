using BusinessObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiClient
{
    public static class CommonExtendedMethod
    {
        public static string ToJson<T>(this IEnumerable<T> objGenericList, bool beautify = false)
            where T : class
        {
            Formatting formatting = beautify ? Formatting.Indented : Formatting.None;
            string jsonFormattedString = JsonConvert.SerializeObject(objGenericList, formatting);
            return jsonFormattedString;
        }

        public static string ToJson<T>(this T objGeneric, bool beautify = false) where T : class
        {
            Formatting formatting = beautify ? Formatting.Indented : Formatting.None;
            string jsonFormattedString = JsonConvert.SerializeObject(objGeneric, formatting);
            return jsonFormattedString;
        }

        public static string ToBase64<T>(this T objGeneric) where T : class
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(objGeneric.ToJson());
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string ToBase64<T>(this IEnumerable<T> objGenericList) where T : class
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(objGenericList.ToJson());
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string ToBase64(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string ToText(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string GetValidDate(this string dateStr)
        {
            try
            {
                dateStr = dateStr.Trim();
                string[] formats = new string[]
                {
                    "MMddyy", "MMddyyyy", "MM/dd/yy", "MM/dd/yyyy","M/dd/yy","M/dd/yyyy","M/d/yy","M/d/yyyy",
                    "MM-dd-yy", "MM-dd-yyyy", "MM.dd.yy", "MM.dd.yyyy",
                    "MM.dd-yy", "MM.dd-yyyy", "MM-dd.yy", "MM-dd.yyyy",

                    "M-d-yy", "M-d-yyyy", "MM-d-yy", "MM-d-yyyy", "M-dd-yy", "M-dd-yyyy",
                    "M.d.yy", "M.d.yyyy", "MM.d.yy", "MM.d.yyyy", "M.dd.yy", "M.dd.yyyy",
					
					// "yyyyMMdd", "yyyy-MM-dd", "yy-MM-dd", 
				};
                DateTime date;
                if (!DateTime.TryParseExact(dateStr, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    dateStr = dateStr.ToLower().Trim().Replace("z", "2");
                    dateStr = dateStr.ToLower().Trim().Replace("s", "5");
                    //dateStr = Regex.Replace(dateStr.ToLower(), "[a-z ]", "");
                    return dateStr;
                }
                return date.ToString("MM/dd/yy");
            }
            catch (Exception) { throw; }
        }

        public static string ToNumString(this string numString)
        {
            return new string(numString.Where(c => char.IsDigit(c)).ToArray());
        }

        public static string ToAlphaNumString(this string alpNumString)
        {
            return new string(alpNumString.Where(c => char.IsLetterOrDigit(c)).ToArray());
        }

        public static bool Match(this IEnumerable<Permission> permissions, Permission permission) => permissions.Any(x => x.DisplayName == permission.DisplayName);
    }
}

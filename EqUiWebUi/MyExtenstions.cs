using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace EqUiWebUi
{
    //string extensions
        public static class MyStringExtensions
        {
        //emulate like operator like in SQL 
            public static bool Like(this string toSearch, string toFind)
            {
                if (toSearch == null) { return false; }
                return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
            }
    }

    //datetime extenstions
    public static class MyDatetimeExtensions
    {
        //round a date to the next hour
        public static DateTime RoundToNearestHour(this DateTime dateTime)
        {
            dateTime += TimeSpan.FromMinutes(30);

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);
        }
    }

    //int extenstions
    public static class MyIntExtensions
    {
        public static int? If0ThenNull(int? input)
        {
            if (input == 0)
            {
                return null;
            }
            else
            {
                return input;
            }
        }

        public static int? If1ThenNull(int? input)
        {
            if (input == 1)
            {
                return null;
            }
            else
            {
                return input;
            }
        }
    }

    //boolean extension
    public static class MyBooleanExtensions
    {
        //check if an sitearea is enabled.
        public static bool IsAreaEnabled(string area)
        {
            if (ConfigurationManager.AppSettings["DisabledAreas"].ToLower().Split(';').Contains(area.ToLower()) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    //data anotations (for mvc model)
    public class HelpTextAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Initializes a new instance of the HelpTextAttribute class with a help message.
        /// </summary>
        /// <param name="helpText">The help text</param>
        public HelpTextAttribute(string helpText) : base(helpText) { }
    }

    //html helper extensions
    public static class HtmlHelperExtensions
    {
        //display helptext donated from the model
        public static string HelpTextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new InvalidOperationException("Expression must be a valid member expression");
            }

            var attributes = memberExpression.Member.GetCustomAttributes(typeof(HelpTextAttribute), true);

            if (attributes.Length == 0)
            {
                return string.Empty;
            }

            var firstAttribute = attributes[0] as HelpTextAttribute;
            return html.Encode(firstAttribute?.Description ?? string.Empty);
        }
    }

}

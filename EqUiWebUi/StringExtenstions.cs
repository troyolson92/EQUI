using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EqUiWebUi
{
        public static class MyStringExtensions
        {
            public static bool Like(this string toSearch, string toFind)
            {
                if (toSearch == null) { return false; }
                return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
            }
        }
 
}

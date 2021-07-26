using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Html
{
    public class HtmlTagHelper
    {
        public const char OPEN_TAG = '<';

        public const char CLOSE_TAG = '>';

        public const string COMMENT_OPEN = "<!---";

        public const string COMMENT_CLOSE = "-->";

        public const string EXTENSION = ".html";

        public static List<string> GetTagContentList(string content)
        {
            var list = new List<string>();
            StringBuilder sb = new StringBuilder();

            var contentToCheck = content.Trim().Replace(COMMENT_OPEN, "").Replace(COMMENT_CLOSE, "");

            if (!contentToCheck.Any(c => c == OPEN_TAG) && !contentToCheck.Any(c => c == CLOSE_TAG))
            {
                list.Add(contentToCheck);
            }
            else if (contentToCheck.Any())
            {
                if (contentToCheck.First() != OPEN_TAG)
                {
                    var text = GetString(contentToCheck.TakeWhile(c => c != OPEN_TAG));
                    list.Add(text);

                    sb.Append(GetString(contentToCheck.SkipWhile(c => c != OPEN_TAG)));
                }
                else
                {
                    sb.Append(GetString(contentToCheck.SkipWhile(c => c != CLOSE_TAG).Skip(1)));
                }

                list = list.Concat(GetTagContentList(sb.ToString())).ToList();
            }

            return list;
        }

        private static string GetString(IEnumerable<char> charList)
        {
            return new string(charList.ToArray());
        }
    }
}

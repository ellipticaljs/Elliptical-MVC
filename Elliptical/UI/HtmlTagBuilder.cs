using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
    public static class HtmlTagBuilder
    {
      
        public static string BeginHtmlTag(string tag, Dictionary<string, string> attributes)
        {
            tag = "<" + tag;
            tag = attrString(tag, attributes);
            tag += ">";
            return tag;
        }

        public static string BeginHtmlTag(string tag, Dictionary<string, string> attributes, bool appendNewLine)
        {
            tag = "<" + tag;
            tag = attrString(tag, attributes);
            tag += ">";

            return (appendNewLine) ? tag + Environment.NewLine : tag;
        }

        public static string BeginHtmlTag(string tag, Dictionary<string, string> attributes, bool appendNewLine, bool selfClose)
        {
            tag = "<" + tag;
            tag = attrString(tag, attributes);
            if (selfClose)
            {
                tag += " />";
            }
            else
            {
                tag += ">";
            }


            return (appendNewLine) ? tag + Environment.NewLine : tag;
        }

        public static string EndHtmlTag(string tag)
        {
            return "</" + tag + ">";
        }

        public static string EndHtmlTag(string tag, bool appendNewLine)
        {
            string endTag = "</" + tag + ">";
            return (appendNewLine) ? endTag + Environment.NewLine : endTag;
        }

        private static string attrString(string tag, Dictionary<string, string> attributes)
        {
            foreach (var pair in attributes)
            {
                if (pair.Key.FirstChars(1) == "@")
                {
                    string key = pair.Key.TrimStart('@');
                    key = key.ToLower();
                    tag += " " + key + "=" + pair.Value.ToWrappedInSingleQuotes();
                }
                else if (pair.Key.ToLower() == "attr")
                {
                    tag += " " + pair.Value;
                }
                else
                {

                    tag += " " + pair.Key + "=" + pair.Value.ToWrappedInQuotes();
                }
            }

            return tag;
        }
        
    }
}
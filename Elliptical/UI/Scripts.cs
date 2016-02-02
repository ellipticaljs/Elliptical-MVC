using System;

namespace Elliptical.Mvc
{
    public static class Script
    {
        public static string EllipticalReady(string script, bool writeScriptTags)
        {
            var ready = "Elliptical(function(){" + Environment.NewLine;
            ready += script + Environment.NewLine;
            ready += "});" + Environment.NewLine;

            if (writeScriptTags)
            {
                var scriptSrc = "<script>" + Environment.NewLine;
                scriptSrc += ready + Environment.NewLine;
                scriptSrc += "</script>" + Environment.NewLine;

                return scriptSrc;
            }
            else
            {
                return ready;
            }
            
        }

        public static string SetTimeout(string script, int delay)
        {
            var timeout = "var self=this;" + Environment.NewLine;
            timeout += setTimeout(script, delay);
            return timeout;
        }

        public static string SetTimeout(string script, int delay, bool initializeSelf)
        {
            if (initializeSelf)
            {
                var timeout = "var self=this;" + Environment.NewLine;
                timeout += setTimeout(script, delay);
                return timeout;
            }
            return setTimeout(script, delay);
        }

        private static string setTimeout(string script, int delay)
        {
            var timeout = "setTimeout(function(){" + Environment.NewLine;
            timeout += script + Environment.NewLine;
            timeout += "}," + delay + ");" + Environment.NewLine;

            return timeout;
        }
    }
}
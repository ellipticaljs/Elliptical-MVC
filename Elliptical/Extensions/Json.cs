using System;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
    static partial class Extensions
    {
        public static string ToJsonString<T>(this T src)
        {
            if (src == null) return null;
            return Json.SerializeObject<T>(src, true, true);
        }
    }
}

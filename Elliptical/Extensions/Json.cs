using System;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
    static partial class Extensions
    {
        public static string toJsonString<T>(this T src)
        {
            return Json.SerializeObject<T>(src, true, true);
        }
    }
}

using System;
using System.Web;


namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public IHtmlString HtmlString(string str)
        {
            return new HtmlString(str);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHtmlString FormatCurrency(decimal value)
        {
            return new HtmlString(formatCurrency(value, true));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="showCurrencyType"></param>
        /// <returns></returns>
        public IHtmlString FormatCurrency(decimal value, bool showCurrencyType)
        {
            return new HtmlString(formatCurrency(value, showCurrencyType));
        }

        private string formatCurrency(decimal value, bool showCurrencyType)
        {
            string strValue = String.Format("{0:C}", value);
            if (!showCurrencyType)
            {
                strValue = strValue.Remove(0, 1);
            }

            return strValue;
        }

    }
}
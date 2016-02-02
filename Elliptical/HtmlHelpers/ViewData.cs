using System;
using System.Web;
using System.Web.Mvc;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
   
    public partial class HtmlHelpers
    {
       private const string WINDOW_VIEWDATA= "window.__viewData";
        
        /// <summary>
        /// Serializes the MVC view model to the browser context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The generic model to serialize</param>
        /// <returns>MvcHtmlString Script Object</returns>
        public IHtmlString ViewData<T>(T model)
        {
            var jsObject = Json.SerializeObject(model);
            var script = scriptObject(this.helper, jsObject, null);
            return new MvcHtmlString(script);
        }


        /// <summary>
        /// Serializes the MVC view model to the browser context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The generic model to serialize</param>
        /// <param name="contextProperty">Assigns the model to this property on the client ViewData context</param>
        /// <returns>MvcHtmlString Script Object</returns>
        public IHtmlString ViewData<T>(T model, string contextProperty)
        {
            var jsObject = Json.SerializeObject(model);
            var script = scriptObject(this.helper, jsObject, contextProperty);
            return new MvcHtmlString(script);
        }


        /// <summary>
        /// Serializes the MVC view model to the browser context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The generic model to serialize</param>
        /// <param name="quoteName">wrap serialized model props in quotes</param>
        /// <param name="camelCase">camelCase serialized model props</param>
        /// <returns></returns>
        public IHtmlString ViewData<T>(T model, bool quoteName, bool camelCase)
        {
            var jsObject = Json.SerializeObject(model, quoteName, camelCase);
            var script = scriptObject(this.helper, jsObject, null);
            return new MvcHtmlString(script);
        }


        /// <summary>
        /// Serializes the MVC view model to the browser context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The generic model to serialize</param>
        /// <param name="contextProperty">Assigns the model to this property on the client ViewData context</param>
        /// <param name="quoteName">wrap serialized model props in quotes</param>
        /// <param name="camelCase">camelCase serialized model props</param>
        /// <returns></returns>
        public IHtmlString ViewData<T>(T model, string contextProperty, bool quoteName, bool camelCase)
        {
            var jsObject = Json.SerializeObject(model, quoteName, camelCase);
            var script = scriptObject(this.helper, jsObject, contextProperty);
            return new MvcHtmlString(script);
        }

        /// <summary>
        /// Assigns a json serialized view model to the browser context
        /// </summary>
        /// <param name="jsonModel">json serialized viewModel string</param>
        /// <returns></returns>
        public IHtmlString JsonViewData(string jsonModel)
        {
            var script = getScript(this.helper, jsonModel, null);
            return new MvcHtmlString(script);
        }

        /// <summary>
        /// Assigns a json serialized view model to the browser context
        /// </summary>
        /// <param name="jsonModel">serialized view model string</param>
        /// <param name="contextProperty">Assigns the model to this property on the client ViewData context</param>
        /// <returns></returns>
        public IHtmlString JsonViewData(string jsonModel, string contextProperty)
        {
            string script = getScript(helper, jsonModel, contextProperty);
            return new MvcHtmlString(script);
        }

        /// <summary>
        /// Assigns a json serialized view model to the browser context
        /// </summary>
        /// <param name="jsonModel">serialized view model string</param>
        /// <param name="includeScriptTag">include opening/closing script tag</param>
        /// <returns></returns>
        public IHtmlString JsonViewData(string jsonModel,bool includeScriptTag)
        {
            string script = "";
            if (includeScriptTag) script = scriptObject(helper, jsonModel, null);
            else script = getScript(helper, jsonModel, null);
            return new MvcHtmlString(script);
        }

        /// <summary>
        /// Assigns a json serialized view model to the browser context
        /// </summary>
        /// <param name="jsonModel">serialized view model string</param>
        /// <param name="contextProperty">Assigns the model to this property on the client ViewData context</param>
        /// <param name="includeScriptTag">include opening/closing script tag</param>
        /// <returns></returns>
        public IHtmlString JsonViewData(string jsonModel,string contextProperty, bool includeScriptTag)
        {
            string script = "";
            if (includeScriptTag) script = scriptObject(helper, jsonModel, contextProperty);
            else script = getScript(helper, jsonModel, contextProperty);
            return new MvcHtmlString(script);
        }

      
        /// <summary>
        /// returns script tag with javascript that assigns the json serialized model to a viewData context on the window object 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="jsObject"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        private string scriptObject(HtmlHelper helper, string jsObject, string contextProperty)
        {
            string script = getScript(helper, jsObject, contextProperty);
            string scriptTag = "<script type='text/javascript'>" + Environment.NewLine;
            return scriptTag + script + Environment.NewLine + "</script>" + Environment.NewLine;
           
        }

        /// <summary>
        /// generates the javascript that assigns the serialized model to a viewData context on the window object
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="jsObject"></param>
        /// <param name="contextProperty"></param>
        /// <returns></returns>
        private string getScript(HtmlHelper helper, string jsObject, string contextProperty)
        {
            string script = "";
            if (contextProperty == null) script += WINDOW_VIEWDATA + "=" + helper.Raw(jsObject) + ";";
            else
            {
                script += WINDOW_VIEWDATA + "=" + WINDOW_VIEWDATA + " || {}; " + Environment.NewLine;
                script += WINDOW_VIEWDATA + "." + contextProperty + "=" + helper.Raw(jsObject) + ";";
            }

            return script;
        }
        

    }
}
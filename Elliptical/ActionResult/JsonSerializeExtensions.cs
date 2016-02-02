using System;
using System.Web.Mvc;
using RevStack.Mvc;


namespace Elliptical.Mvc
{
    public static class JsonSerializeExtensions
    {
        private const string JSON_VIEW_MODEL = "_JSON_VIEW_MODEL";
        private const string JSON_VIEW_MODEL_PROP = "_JSON_VIEW_MODEL_PROP";
        public static string GetViewModel(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(JSON_VIEW_MODEL))
            {
                tempData[JSON_VIEW_MODEL] = null;
            }

            return (string)tempData[JSON_VIEW_MODEL];
        }

        public static string GetViewModelProp(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(JSON_VIEW_MODEL_PROP))
            {
                tempData[JSON_VIEW_MODEL_PROP] = null;
            }

            return (string)tempData[JSON_VIEW_MODEL_PROP];
        }

        public static void SetViewModel(this TempDataDictionary tempData, string jsonModel)
        {
            tempData[JSON_VIEW_MODEL] = jsonModel;
        }

        public static void SetViewModelProp(this TempDataDictionary tempData, string contextProperty)
        {
            tempData[JSON_VIEW_MODEL_PROP] = contextProperty;
        }

        public static ViewResult WithSerialization<T>(this ViewResult result)
        {
            var model = (T)result.Model;
            var jsonModel = Json.SerializeObject(model);
            return new JsonSerializeViewResult(result, jsonModel,null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">View Model</param>
        /// <param name="contextProperty">Assigns the model to this property on the client ViewData context</param>
        /// <returns></returns>
        public static ViewResult WithSerialization<T>(this ViewResult result,string contextProperty)
        {
            var model = (T)result.Model;
            var jsonModel = Json.SerializeObject(model);
            return new JsonSerializeViewResult(result, jsonModel,contextProperty);
        }
    }
}

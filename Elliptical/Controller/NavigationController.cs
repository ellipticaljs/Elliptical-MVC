using System;
using System.Collections.Generic;
using System.Web.OData.Query;
using RevStack.Pattern;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
    public class NavigationController<TEntity,TKey> : ResponseController
        where TEntity : class, IEntity<TKey>
    {

        protected virtual EntityNavigation<TEntity> PageNavigation(TKey id,IEnumerable<TEntity> collection,string baseUrl)
        {
            var odata = new OData<TEntity, TKey>();
            var options = odata.ODataQueryOptions();
            ODataQuerySettings settings = new ODataQuerySettings() { };
            var page = odata.PageNavigation(collection, id, options, settings);
            if (page.NextItem != null) page.NextPageLink = baseUrl + "/" + page.NextItem.Id + page.UrlQueryString;
            if (page.PrevItem != null) page.PrevPageLink = baseUrl + "/" + page.PrevItem.Id + page.UrlQueryString;

            return page;
        }
    }
}

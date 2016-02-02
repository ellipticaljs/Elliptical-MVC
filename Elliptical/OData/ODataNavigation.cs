using System;
using System.Collections.Generic;


namespace Elliptical.Mvc
{
    public class ODataNavigation<TEntity>
    {
        public TEntity NextItem { get; set; }
        public TEntity PrevItem { get; set; }
        public string NextPageLink { get; set; }
        public string PrevPageLink { get; set; }
        public string UrlQueryString { get; set; }
        public int Count { get; set; }

        public ODataNavigation()
        {
            NextItem = default(TEntity);
            PrevItem = default(TEntity);
            NextPageLink = null;
            PrevPageLink = null;
            UrlQueryString = null;
            Count = 0;
        }
    }
}

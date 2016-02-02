using System;
using System.Collections.Generic;
using System.Linq;

namespace Elliptical.Mvc
{
    public class Pagination
    {
        public Pagination()
        {
            BaseUrl = "";
            Count = 0;
            PageSize = 12;
            Page = 1;
            PageSpread = 10;
            CssClass = null;
            TagName = null;
        }

        public Pagination(string url, int count, int pageSize)
        {
            BaseUrl = url;
            Count = count;
            PageSize = pageSize;
            Page = 1;
            PageSpread = 10;
            CssClass = null;
            TagName = null;
        }

        public Pagination(string url, int count, int pageSize, int page)
        {
            BaseUrl = url;
            Count = count;
            PageSize = pageSize;
            Page = page;
            PageSpread = 10;
            CssClass = null;
            TagName = null;
        }

        public Pagination(string url, int count, int pageSize, int page, int pageSpread)
        {
            BaseUrl = url;
            Count = count;
            PageSize = pageSize;
            Page = page;
            PageSpread = pageSpread;
            CssClass = null;
            TagName = null;
        }

        public Pagination(string url, int count, int pageSize, int page, int pageSpread, string cssClass)
        {
            BaseUrl = url;
            Count = count;
            PageSize = pageSize;
            Page = page;
            PageSpread = pageSpread;
            CssClass = cssClass;
            TagName = null;
        }

        public Pagination(string url, int count, int pageSize, int page, int pageSpread, string cssClass, string tagName)
        {
            BaseUrl = url;
            Count = count;
            PageSize = pageSize;
            Page = page;
            PageSpread = pageSpread;
            CssClass = cssClass;
            TagName = tagName;
        }

        public string BaseUrl { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int PageSpread { get; set; }
        public string CssClass { get; set; }
        public string TagName { get; set; }
    }

    public class PagedViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class PagedModel<T>
    {
        public IQueryable<T> Items { get; set; }
        public long? Count { get; set; }
        public Uri NextPageLink { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

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

    public class PageItem
    {
        public int Page { get; set; }
        public string Link { get; set; }
        public string Active { get; set; }

        public PageItem()
        {
            Active = "";
        }
    }

    public class PagedEnumerable<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public string NextPageLink { get; set; }
        public string PrevPageLink { get; set; }
        public int PageSpread { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<PageItem> PageItems { get; set; }
    }

    public static class PaginationUtility
    {
        public static string QueryString(NameValueCollection query)
        {
            if(query==null)
            {
                return "";
            }
            string queryString = "?";
            var index = 0;
            foreach (string key in query)
            {
                if(key !="page")
                {
                    string value = WebUtility.UrlDecode(query[key]);
                    queryString = (index > 0) ? queryString + "&" + key + "=" + value : queryString + key + "=" + value;
                    index++;
                }
            }

            return queryString;
        }

        public static IEnumerable<PageItem> GeneratePages(string baseUrl, int page, int pageCount, int pageSize, int pageSpread, string queryString)
        {
            var pages = new List<PageItem>();
           

            if (pageSpread > pageCount)
            {
                pageSpread = pageCount;
            }
            if(queryString==null)
            {
                queryString = "";
            }
            if (page < pageSpread)
            {

                for (var i = 0; i < pageSpread; i++)
                {
                    var pageItem = new PageItem
                    {
                        Page = i + 1,
                        Link = baseUrl + queryString.ToPageQueryString(page)
                    };

                    if (i == (page - 1))
                    {
                        pageItem.Active = "active";
                    }

                    pages.Add(pageItem);
                }
            }
            else
            {
                int halfSpread = (pageSpread / 2);
                int beginPage, endPage;
                if (pageCount < page + halfSpread)
                {
                    endPage = pageCount;
                    beginPage = endPage - pageSpread;
                }
                else
                {
                    endPage = page + halfSpread;
                    beginPage = page - halfSpread;
                }
                if (beginPage == 0) beginPage = 1;
                for (var i = beginPage; i < endPage + 1; i++)
                {
                    var pageItem = new PageItem
                    {
                        Page = i + 1,
                        Link = baseUrl + queryString.ToPageQueryString(page)
                    };
                    if (i == page)
                    {
                        pageItem.Active = "active";
                    }
                    pages.Add(pageItem);
                }
            }

            return pages;
        } 

        public static int GetPageCount(int count, int pageSize)
        {
            int pageCount = (count / pageSize);
            int remainder = count % pageSize;
            if (pageCount < 1) pageCount = 1;
            else if (remainder > 0) pageCount++;

            return pageCount;
        }
    }
}
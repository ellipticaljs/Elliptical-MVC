using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;


namespace Elliptical.Mvc
{
    public partial class HtmlHelpers
    {
        /// <summary>
        /// UI Pagination Component
        /// </summary>
        /// <param name="baseUrl">Page Url</param>
        /// <param name="count">Model total count</param>
        /// <param name="pageSize">Number of items to display per page</param>
        /// <param name="page">Current page</param>
        /// <returns>MvcHtmlString</returns>
        public IHtmlString UIPagination(string baseUrl, int count, int pageSize, int page)
        {
            return uiPagination(baseUrl, count, pageSize, page, 10, null, null, PaginationAlignment.Left);
        }


        /// <summary>
        ///  UI Pagination Component method overload
        /// </summary>
        /// <param name="baseUrl">Page Url</param>
        /// <param name="count">Model total count</param>
        /// <param name="pageSize">Number of items to display per page</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSpread">Max Number of page links to display</param>
        /// <returns>MvcHtmlString</returns>
        public IHtmlString UIPagination(string baseUrl, int count, int pageSize, int page, int pageSpread)
        {
            return uiPagination(baseUrl, count, pageSize, page, pageSpread, null, null, PaginationAlignment.Left);
        }


        /// <summary>
        ///  UI Pagination Component method overload
        /// </summary>
        /// <param name="baseUrl">Page Url</param>
        /// <param name="count">Model total count</param>
        /// <param name="pageSize">Number of items to display per page</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSpread">Max Number of page links to display</param>
        /// <param name="cssClass">Component css class attribute value</param>
        /// <returns>MvcHtmlString</returns>
        public IHtmlString UIPagination(string baseUrl, int count, int pageSize, int page, int pageSpread, string cssClass)
        {
            return uiPagination(baseUrl, count, pageSize, page, pageSpread, cssClass, null, PaginationAlignment.Left);
        }


        /// <summary>
        ///  UI Pagination Component method overload
        /// </summary>
        /// <param name="baseUrl">Page Url</param>
        /// <param name="count">Model total count</param>
        /// <param name="pageSize">Number of items to display per page</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSpread">Max Number of page links to display</param>
        /// <param name="cssClass">Component css class attribute value</param>
        /// <param name="tagName">custom tag name</param>
        /// <returns>MvcHtmlString</returns>
        public IHtmlString UIPagination(string baseUrl, int count, int pageSize, int page, int pageSpread, string cssClass, string tagName)
        {
            return uiPagination(baseUrl, count, pageSize, page, pageSpread, cssClass, tagName, PaginationAlignment.Left);
        }

        /// <summary>
        /// UI Pagination Component method overload
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="pageSpread"></param>
        /// <param name="cssClass"></param>
        /// <param name="tagName"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public IHtmlString UIPagination(string baseUrl, int count, int pageSize, int page, int pageSpread, string cssClass, string tagName, PaginationAlignment alignment)
        {
            return uiPagination(baseUrl, count, pageSize, page, pageSpread, cssClass, tagName, alignment);
        }

        /// <summary>
        /// UI Pagination Component method overload
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IHtmlString UIPagination(Pagination pagination)
        {
            return uiPagination(pagination.BaseUrl, pagination.Count, pagination.PageSize, pagination.Page, pagination.PageSpread, pagination.CssClass, pagination.TagName, PaginationAlignment.Left);
        }


        /// <summary>
        /// UI Pagination Component method overload
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public IHtmlString UIPagination(Pagination pagination, PaginationAlignment alignment)
        {
            return uiPagination(pagination.BaseUrl, pagination.Count, pagination.PageSize, pagination.Page, pagination.PageSpread, pagination.CssClass, pagination.TagName, alignment);
        }

         /// <summary>
        ///  Private method that generates the UI Pagination Component for all public overload methods
        /// </summary>
        /// <param name="baseUrl">Page Url</param>
        /// <param name="count">Model total count</param>
        /// <param name="pageSize">Number of items to display per page</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSpread">Max Number of page links to display</param>
        /// <param name="cssClass">Component css class attribute value</param>
        /// <param name="tagName">custom tag name</param>
        /// <returns>MvcHtmlString</returns>
        private IHtmlString uiPagination(string baseUrl, int count, int pageSize, int page, int pageSpread, string cssClass, string tagName, PaginationAlignment alignment)
        {

            var element = "";
            tagName = tagName ?? "ui-pagination";
            var endTag = "</" + tagName + ">";
            if (cssClass == null)
            {
                if (alignment == PaginationAlignment.Left)
                {
                    element = "<" + tagName + ">";
                }
                else
                {
                    element = "<" + tagName + " class='center'>";
                }
            }
            else
            {
                if (alignment == PaginationAlignment.Left)
                {
                    element = "<" + tagName + " class='" + cssClass + "'>";
                }
                else
                {
                    element = "<" + tagName + " class='" + cssClass + " center'>";
                }
            }

            var prevPage = new PageLink();
            prevPage.CssClass = "hide";
            var nextPage = new PageLink();
            nextPage.CssClass = "hide";
            int pageCount = count / pageSize;
            int remainder = count % pageSize;
            if (pageCount < 1)
            {
                pageCount = 1;
            }
            else if (remainder > 0)
            {
                pageCount++;
            }

            element += "<div class='page-info'>Page";
            element += "<span class='page-no'> " + page + "</span> of <span class='page-count'>" + pageCount + "</span>";
            element += "</div>";
            element += "<ul class='right'>";

            //prev
            if (page > 1)
            {
                prevPage.CssClass = "";
                prevPage.Url = pageUrl(baseUrl, page - 1);
                element += "<li><a href='" + prevPage.Url + "'>prev</a></li>";
            }

            //pages
            if (pageCount > 1)
            {
                var pages = pageLinks(baseUrl, page, pageCount, pageSpread);
                foreach (PageLink p in pages)
                {
                    element += "<li><a class='" + p.Active + "' href='" + p.Url + "'>" + p.Page + "</a></li>";
                }
            }

            //next
            if (page < pageCount)
            {
                nextPage.CssClass = "";
                nextPage.Url = pageUrl(baseUrl, page + 1);
                element += "<li><a href='" + nextPage.Url + "'>next</a></li>";
            }


            element += "</ul>" + endTag;


            return new MvcHtmlString(element);

        }

        /// <summary>
        /// returns list of PageLink
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="page"></param>
        /// <param name="pageCount"></param>
        /// <param name="pageSpread"></param>
        /// <returns></returns>
        private List<PageLink> pageLinks(string baseUrl, int page, int pageCount, int pageSpread)
        {
            var pages = new List<PageLink>();
            if (pageSpread > pageCount)
            {
                pageSpread = pageCount;
            }
            if (page <= pageSpread)
            {
                for (var i = 0; i < pageSpread; i++)
                {
                    var obj = new PageLink();
                    int _page = i + 1;
                    obj.Page = _page;
                    obj.Url = pageUrl(baseUrl, _page);

                    if (i == (page - 1))
                    {
                        obj.Active = "active";
                    }
                    pages.Add(obj);
                }

                return pages;

            }
            else
            {
                var halfSpread = (pageSpread / 2);
                int beginPage;
                int endPage;
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

                for (var i = beginPage; i < endPage + 1; i++)
                {
                    var obj = new PageLink();
                    obj.Page = i;
                    obj.Url = pageUrl(baseUrl, i);
                    if (i == page)
                    {
                        obj.Active = "active";
                    }
                    pages.Add(obj);
                }

                return pages;
            }
        }

        /// <summary>
        /// returns a page url
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private string pageUrl(string baseUrl, int page)
        {

            return (baseUrl.IndexOf("?") > -1) ? baseUrl + "&page=" + page : baseUrl + "?page=" + page;
        }

       



    }

    /// <summary>
    /// internal PageLink class
    /// </summary>
    internal class PageLink
    {
        public string Url { get; set; }
        public int Page { get; set; }
        public string Active { get; set; }
        public string CssClass { get; set; }

        public PageLink()
        {
            Url = "";
            Page = 1;
            Active = "";
            CssClass = "";
        }
    }

}
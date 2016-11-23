using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Net;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Extensions;
using System.Net.Http;
using System.Web.OData.Builder;
using RevStack.Pattern;
using RevStack.Mvc;

namespace Elliptical.Mvc
{
    public class OData<TEntity,TKey> where TEntity : class, IEntity<TKey>
    {
        private Type _type;

        public OData()
        {
            _type= typeof(TEntity);
        }
        public string QueryStringMapper(NameValueCollection query)
        {
            string queryString = filter(query);
            bool q = false;
            string sep = "?";
            if (!string.IsNullOrEmpty(queryString))
            {
                q = true;
                queryString = sep + queryString;
            }
            string orderby = orderBy(query);
            if (!string.IsNullOrEmpty(orderby))
            {
                if (q == true) sep = "&";
                else sep = "?";
                q = true;
                queryString = queryString + sep + orderby;
            }
            string orderbydesc = orderByDesc(query);
            if (!string.IsNullOrEmpty(orderbydesc))
            {
                if (q == true) sep = "&";
                else sep = "?";
                q = true;
                queryString = queryString + sep + orderbydesc;
            }
            string _top = top(query);
            if (!string.IsNullOrEmpty(_top))
            {
                if (q == true) sep = "&";
                else sep = "?";
                q = true;
                queryString = queryString + sep + _top;
            }

            string _skip = skip(query);
            if (!string.IsNullOrEmpty(_skip))
            {
                if (q == true) sep = "&";
                else sep = "?";
                q = true;
                queryString = queryString + sep + _skip;
            }

            return queryString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ODataQueryOptions ODataQueryOptions()
        {
            var context=new HttpContextWrapper(HttpContext.Current);
            var request = context.Request;
            string queryString = QueryStringMapper(request.QueryString);
            var baseUrl = new UriBuilder(request.Url.Scheme, request.Url.Host, request.Url.Port, request.Path);
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, baseUrl + queryString);
            return getOptions(queryString, req);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl">protocol,host,path</param>
        /// <param name="queryString">proper odata formatted query string</param>
        /// <returns></returns>
        public ODataQueryOptions ODataQueryOptions(UriBuilder baseUrl,string queryString)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl + queryString);
            return getOptions(queryString, request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ODataQueryOptions ODataQueryOptions(HttpRequestBase request)
        {
            string queryString = QueryStringMapper(request.QueryString);
            var baseUrl = new UriBuilder(request.Url.Scheme, request.Url.Host, request.Url.Port, request.Path);
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, baseUrl + queryString);
            return getOptions(queryString, req);
        }

        /// <summary>
        /// Returns an IEnumerable Page Result
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> PageResult(IEnumerable<TEntity> query,ODataQueryOptions options,ODataQuerySettings settings)
        {
            IQueryable q = options.ApplyTo(query.AsQueryable(), settings);
            var pagedResult = new PageResult<TEntity>(q as IQueryable<TEntity>, null, null);
            return pagedResult.Items;
        }

        /// <summary>
        /// Returns an PagedEnumerable Object of an entity collection
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public PagedEnumerable<TEntity> PageResult(IEnumerable<TEntity> query,string baseUrl,HttpRequestBase request,int pageSize,int? pageSpread)
        {
            
            var pagedEnumerable = new PagedEnumerable<TEntity>();
            NameValueCollection queryString = request.QueryString;
            var uri = new UriBuilder(request.Url.Scheme, request.Url.Host, request.Url.Port, baseUrl);
            int currentPage = 1;
            if(queryString["page"] !=null)
            {
                currentPage = Convert.ToInt32(queryString["page"]);
            }
            string odataQueryString = QueryStringMapper(queryString);
            string strQueryString = PaginationUtility.QueryString(queryString);
            var options = ODataQueryOptions(uri, odataQueryString);
            var settings = new ODataQuerySettings();
            IQueryable q = options.ApplyTo(query.AsQueryable(), settings);
            var pagedResult = new PageResult<TEntity>(q as IQueryable<TEntity>,null,null);
            var pagedItemsList = pagedResult.Items.ToList();
            int count = pagedItemsList.Count();
            pagedEnumerable.Items = pagedItemsList.ToPagedList(currentPage,pageSize);
            pagedEnumerable.Count = count;
            pagedEnumerable.PageSize = pageSize;
            int pageCount = PaginationUtility.GetPageCount(count, pageSize);
            pagedEnumerable.PageSpread = 0;
            pagedEnumerable.CurrentPage = currentPage;
            pagedEnumerable.PageCount = pageCount;
            int intPageSpread;
            if(count > pageSize)
            {
                if(pageCount > currentPage)
                {
                    pagedEnumerable.NextPageLink = baseUrl + strQueryString.ToPageQueryString(currentPage + 1);
                }
                if (pageSpread != null)
                {
                    intPageSpread = Convert.ToInt32(pageSpread);
                    pagedEnumerable.PageSpread = intPageSpread;
                    pagedEnumerable.PageItems = PaginationUtility.GeneratePages(baseUrl, currentPage, pageCount, pageSize, intPageSpread, strQueryString);

                }
                if(currentPage > 1)
                {
                    pagedEnumerable.PrevPageLink = baseUrl + strQueryString.ToPageQueryString(currentPage - 1);
                }
            }

            return pagedEnumerable;

        }

        /// <summary>
        /// Returns a navigation object for Paging through an odata result set one at a time
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public EntityNavigation<TEntity> PageNavigation(IEnumerable<TEntity> query,TKey id, ODataQueryOptions options, ODataQuerySettings settings)
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            var request = context.Request;
            return getNavigation(query, id, options, settings, request.QueryString);
        }

        #region "Private"
        private EntityNavigation<TEntity> getNavigation(IEnumerable<TEntity> query, TKey id, ODataQueryOptions options, ODataQuerySettings settings,NameValueCollection querystring)
        {
            var items = PageResult(query, options, settings);
            var navigation = new EntityNavigation<TEntity>();
            if (items != null)
            {
                navigation.UrlQueryString = "?" + HttpUtility.ParseQueryString(querystring.ToString());
                navigation.Count = items.Count();
                navigation.NextItem = items.SkipWhile(x => x.Id.ToString() != id.ToString()).Skip(1).FirstOrDefault();
                navigation.PrevItem = items.Reverse().SkipWhile(x => x.Id.ToString() != id.ToString()).Skip(1).FirstOrDefault();
            }

            return navigation;
        }
        private ODataQueryOptions getOptions(string queryString,HttpRequestMessage request)
        {
            string type = _type.ToString();
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.ComplexType<TEntity>();
            ODataQueryContext context = new ODataQueryContext(builder.GetEdmModel(), _type, request.ODataProperties().Path);
            return new ODataQueryOptions<TEntity>(context, request);
        }

        private string paginationFilter(string queryString,int page, int pageSize)
        {
            page=page-1;
            int skip = page * pageSize;
            string encodedPaginate = (skip > 0) ? "$skip=" + skip + "&$top=" + pageSize + "&$count=true" : "$top=" + pageSize + "&$count=true";
            if(queryString.IndexOf("?") > -1)
            {
                queryString = queryString + "&";
            }
            else
            {
                queryString = queryString + "?";
            }
            return queryString + encodedPaginate;
        }

        private string filter(NameValueCollection query)
        {
            string str = "";
            int checksum = 0;
            foreach (string key in query)
            {
                string value = WebUtility.UrlDecode(query[key]);
                string prop;
                if (key.IndexOf("sw_") == 0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and startswith(" + prop + ",'" + value + "')" : "startswith(" + prop + ",'" + value + "')";
                    checksum++;
                }
                else if (key.IndexOf("swl_") == 0)
                {
                    prop = key.Substring(4);
                    str += (checksum > 0) ? " and startswith(tolower(" + prop + "),tolower('" + value + "'))" : "startswith(tolower(" + prop + "),tolower('" + value + "'))";
                    checksum++;
                }
                else if (key.IndexOf("swu_") == 0)
                {
                    prop = key.Substring(4);
                    str += (checksum > 0) ? " and startswith(toupper(" + prop + "),toupper('" + value + "'))" : "startswith(toupper(" + prop + "),toupper('" + value + "'))";
                    checksum++;
                }
                else if (key.IndexOf("c_") == 0)
                {
                    prop = key.Substring(2);
                    str += (checksum > 0) ? " and contains(" + prop + ",'" + value + "')" : "contains(" + prop + ",'" + value + "')";
                    checksum++;
                }
                else if (key.IndexOf("cl_") == 0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and contains(tolower(" + prop + "),tolower('" + value + "'))" : "contains(tolower(" + prop + "),(tolower('" + value + "'))";
                    checksum++;
                }
                else if (key.IndexOf("cu_") == 0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and contains(toupper(" + prop + "),toupper('" + value + "'))" : "contains(toupper(" + prop + "),(toupper('" + value + "'))";
                    checksum++;
                }
                else if (key.IndexOf("ew_") == 0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and endswith(" + prop + ",'" + value + "')" : "endswith(" + prop + ",'" + value + "')";
                    checksum++;
                }
                else if (key.IndexOf("ewl_") == 0)
                {
                    prop = key.Substring(4);
                    str += (checksum > 0) ? " and endswith(tolower(" + prop + "),tolower('" + value + "'))" : "endswith(tolower(" + prop + "),tolower('" + value + "'))";
                    checksum++;
                }
                else if (key.IndexOf("ewu_") == 0)
                {
                    prop = key.Substring(4);
                    str += (checksum > 0) ? " and endswith(toupper(" + prop + "),toupper('" + value + "'))" : "endswith(toupper(" + prop + "),toupper('" + value + "'))";
                    checksum++;
                }
                else if(key.IndexOf("eq_")==0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and " + prop + " eq " + value : prop + " eq " + value;
                    checksum++;
                }
                else if (key.IndexOf("eql_") == 0)
                {
                    prop = key.Substring(4);
                    str += (checksum > 0) ? " and tolower(" + prop + ") eq tolower('" + value + "')" : "tolower(" + prop + ") eq tolower('" + value + "')";
                    checksum++;
                }
                else if (key.IndexOf("equ_") == 0)
                {
                    prop = key.Substring(4);
                    str += (checksum > 0) ? " and toupper(" + prop + ") eq toupper('" + value + "')" : "toupper(" + prop + ") eq toupper('" + value + "')";
                    checksum++;
                }
                else if(key.IndexOf("lt_")==0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and " + prop + " lt " + value : prop + " lt " + value;
                    checksum++;
                }
                else if (key.IndexOf("gt_") == 0)
                {
                    prop = key.Substring(3);
                    str += (checksum > 0) ? " and " + prop + " gt " + value : prop + " gt " + value;
                    checksum++;
                }
                else if (key.IndexOf("cq_") == 0)
                {
                    str += (checksum > 0) ? " and " + value : value;
                    checksum++;
                }
                else if (key.IndexOf("search_") == 0)
                {
                    prop = key.Substring(7);
                    var props = prop.Split('_');
                    string search = "";
                    for (var i = 0; i < props.Count(); i++)
                    {
                        var _prop = props[i];
                        if (i > 0) search += " or ";
                        search += "contains(tolower(" + _prop + "),tolower('" + value + "'))";
                    }
                    str += (checksum > 0) ? " and " + search : search;
                    checksum++;
                }
                else if ((key.IndexOf("$") != 0)  && key.ToLower() != "page")
                {
                    str += (checksum > 0) ? " and " + key + " eq '" + value + "'" : key + " eq '" + value + "'";
                    checksum++;
                }
            }

            if (string.IsNullOrEmpty(str)) return str;
            return "$filter=" + WebUtility.UrlEncode(str);
        }

        private string orderBy(NameValueCollection query)
        {
            string str = "";
            string value = query["$orderBy"];
            if (!string.IsNullOrEmpty(value))
            {
                if(value.IndexOf(".") > -1)
                {
                    value = getNestedQueryProps(value);
                }
                str = "$orderby=" + WebUtility.UrlEncode(value);
            }
            return str;

        }

        private string orderByDesc(NameValueCollection query)
        {
            string str = "";
            string value = query["$orderByDesc"];
            if (!string.IsNullOrEmpty(value))
            {
                if (value.IndexOf(".") > -1)
                {
                    value = getNestedQueryProps(value);
                }
                str = "$orderby=" + WebUtility.UrlEncode(value) + " desc";
            }
            return str;
        }


        private string top(NameValueCollection query)
        {
            string str = "";
            string value = query["$top"];
            if (!string.IsNullOrEmpty(value))
            {
                str = "$top=" + value;
            }
            return str;
        }

        private string skip(NameValueCollection query)
        {
            string str = "";
            string value = query["$skip"];
            if (!string.IsNullOrEmpty(value))
            {
                str = "$skip=" + value;
            }
            return str;
        }

        private string getNestedQueryProps(string nestedProp)
        {
            nestedProp = nestedProp.Replace(".", "/");
            return nestedProp;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;

namespace Elliptical.Mvc
{
    public static partial class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToCamelCaseFromProperCase(this string source)
        {
            return source.Substring(0, 1).ToLower() + source.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToHtmlFilteredString(this string source)
        {
            return Regex.Replace(source, @"<(.|\n)*?>", string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="spaceFilterBrTag"></param>
        /// <returns></returns>
        public static string ToHtmlFilteredString(this string source, bool spaceFilterBrTag)
        {
            if (spaceFilterBrTag)
            {
                string filtered = source.Replace("<br>", " ");
                return Regex.Replace(filtered, @"<(.|\n)*?>", string.Empty);
            }
            else
            {
                return Regex.Replace(source, @"<(.|\n)*?>", string.Empty);
            }
        }



        /// <summary>
        /// extension that returns a page subset list from a list instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<T> ToPagedList<T>(this List<T> model, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return model.AsEnumerable()
                .Skip(skip).Take(pageSize)
                .ToList();
        }

        internal static MetaSelectModel ToMetaSelectModel<TModel,TResult>(this Expression<Func<TModel, TResult>> expression)
        {
            var dictionary = new Dictionary<string, string>();
            
            var initExpression = expression.Body as MemberInitExpression;
            IEnumerable<MemberBinding> bindings = initExpression.Bindings;
            foreach (var memberBinding in bindings)
            {
                var memberAssigment = memberBinding as MemberAssignment;
                var member = memberAssigment.Member;
                var name = member.Name;
                var mExpression = memberAssigment.Expression as MemberExpression;
                var value = mExpression.Member.Name;

                dictionary.Add(name, value);

            }

            return new MetaSelectModel
            {
                Options = dictionary["Options"],
                SelectedProperty = dictionary["SelectedProperty"]
            };
        }

        public static string PropertyName<T>(this Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }

      

        private static object GetPropertyValue<T>(T instance, MemberExpression me)
        {

            object target;

            if (me.Expression.NodeType == ExpressionType.Parameter)
            {
                // If the current MemberExpression is at the root object, set that as the target.            
                target = instance;
            }
            else
            {
                target = GetPropertyValue<T>(instance, me.Expression as MemberExpression);
            }

            // Return the value from current MemberExpression against the current target
            return target.GetType().GetProperty(me.Member.Name).GetValue(target, null);

        }
    }
}
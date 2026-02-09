using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace csa.Data.Library
{
    public static class ExtensionMethod
    {
        public static IOrderedQueryable<T> AddOrdering<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector, SortDirection sortDirection)
        {
            if (source.Expression.Type != typeof(IOrderedQueryable<T>))
            {
                return (sortDirection == SortDirection.Ascending) ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
            }

            return (sortDirection == SortDirection.Ascending) ? ((IOrderedQueryable<T>)source).ThenBy(keySelector) : ((IOrderedQueryable<T>)source).ThenByDescending(keySelector);
        }
    }
}

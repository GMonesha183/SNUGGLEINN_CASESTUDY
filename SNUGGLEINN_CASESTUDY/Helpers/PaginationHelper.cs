using System;
using System.Linq;

namespace SNUGGLEINN_CASESTUDY.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<T> Paginate<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}

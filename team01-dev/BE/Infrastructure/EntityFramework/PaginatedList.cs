using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int TotalItem { get; private set; }
        public int TotalPage { get; private set; }
        public int PageSize { get; private set; }
        public List<T> Results { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPage;

        public PaginatedList()
        {
        }
        public PaginatedList(IQueryable<T> items, int skip = 0, int take = 50)
        {
            if (items.IsNotNullOrEmpty())
            {
               
                PageIndex = (int)Math.Round((decimal)skip / take, 0);
                PageSize = take;
                TotalItem = items.Count();
                TotalPage = (int)Math.Ceiling(TotalItem / (double)PageSize);
                GetPageData(items);
            }
        }

        public void GetPageData(IQueryable<T> items)
        {
            Results = items?.Skip(PageSize * PageIndex).Take(PageSize).ToList() ?? new List<T>();
        }
    }
}

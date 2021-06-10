using System.Linq;

namespace Common.Pagination
{
    public class SearchPaginationDTO<T>
    {

        public T Search { get; set; }
        public int PageIndex
        {
            get; set;
        }
        public int PageSize { get; set; } = 10;

    }

}

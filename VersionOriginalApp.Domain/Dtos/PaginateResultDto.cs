using System.Collections.Generic;

namespace VersionOriginalApp.Domain.Dtos
{
    public class PaginateResultDto<T>
    {
        public int TotalOfItems { get; set; }
        public int TotalOfPages { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}

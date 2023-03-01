using System;

namespace backend.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        private int pageNumber = 0;
        private int pageSize = 0;

        public int PageNumber { get => pageNumber; set => pageNumber = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }



    }
}

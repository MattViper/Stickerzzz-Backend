using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stickerzzz.Web.Responses
{
    public class ApiSuccessResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiSuccessResponse(bool success, T responseData)
            : base(success)
        {
            Data = responseData;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Stickerzzz.Infrastructure.Errors
{
    public class RestException : Exception
    {
        public object Errors { get; set; }
        public HttpStatusCode Code { get; set; }

        public RestException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}

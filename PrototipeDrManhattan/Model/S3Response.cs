using System;
using System.Net;

namespace S3TestWebApi.Model
{
    public class S3Response
    {
        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }

    }
}

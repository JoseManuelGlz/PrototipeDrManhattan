using System;
using System.Collections.Generic;
using System.Net;

namespace S3TestWebApi.Model
{
    public class S3ListBucketResponse
    {
        public HttpStatusCode status { get; set; }

        public IEnumerable<string> buckets { get; set; }

        public string error { get; set; }
    }
}

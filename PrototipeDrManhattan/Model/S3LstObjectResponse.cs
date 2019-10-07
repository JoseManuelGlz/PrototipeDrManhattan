using System;
using System.Collections.Generic;
using System.Net;
using Amazon.S3;

namespace S3TestWebApi.Model {
    public class S3LstObjectResponse {
        public HttpStatusCode status { get; set; }
        public IEnumerable<S3ObjectResponse> s3Objects { get; set; } 
        public S3LstObjectResponse (HttpStatusCode status, IEnumerable<S3ObjectResponse> s3Objects) {
            this.status = status;
            this.s3Objects = s3Objects;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Net;
using Amazon.S3;

namespace S3TestWebApi.Model {
    public class S3ObjectResponse {
        public string bucketName { get; set; }
        public string eTag { get; set; }
        public string key { get; set; }
        public DateTime lastModified { get; set; }
        public long size { get; set; }
        public S3StorageClass storageClass { get; set; }
        public S3ObjectResponse (string bucketName, string eTag, string key, DateTime lastModified, long size, S3StorageClass storageClass)
        {
            this.bucketName = bucketName;
            this.eTag = eTag;
            this.key = key;
            this.lastModified = lastModified;
            this.size = size;
            this.storageClass = storageClass;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using S3TestWebApi.Model;

namespace S3TestWebApi.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                var exist = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);
                if(!exist)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    var response = await _client.PutBucketAsync(putBucketRequest);

                    return new S3Response
                    {
                        Message = response.ResponseMetadata.RequestId,
                        Status = response.HttpStatusCode
                    };
                }
                return new S3Response
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message  = ""
                };
            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Status = e.StatusCode,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                return new S3Response
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = e.Message
                };
            }
        }

        public async Task<S3ListBucketResponse> listBuckets()
        {
            try
            {
                var lstBuckets = await _client.ListBucketsAsync();

                return new S3ListBucketResponse
                {
                    buckets = lstBuckets.Buckets.Select(x => x.BucketName).ToList()
                };
            }
            catch (AmazonS3Exception ex)
            {
                return new S3ListBucketResponse
                {
                    error = ex.Message
                };
            }
        }
    
        public async Task<S3LstObjectResponse> listObjectFromBucket(string bucketName)
        {
            try
            {
                var lstRequestObject = new ListObjectsV2Request();

                lstRequestObject.BucketName=bucketName;

                var lstObject = await _client.ListObjectsV2Async(lstRequestObject);

                var lstResponse = lstObject.S3Objects.Select(x => new S3ObjectResponse(x.BucketName,x.ETag,x.Key,x.LastModified,x.Size,x.StorageClass));

                return new S3LstObjectResponse(lstObject.HttpStatusCode, lstResponse);
            }
            catch(AmazonS3Exception  ex)
            {
                return new S3LstObjectResponse (ex.StatusCode,null);
            }
        }
        public async Task<S3Response> UploadFileAsync(Stream fileToUpload, string bucketName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_client);

                await fileTransferUtility.UploadAsync(fileToUpload, bucketName, bucketName);

            }

            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = e.StatusCode
                };
            }

            catch (Exception e)
            {
                return new S3Response
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError
                };
            }

            return new S3Response
            {
                Message = "File uploaded Successfully",
                Status = HttpStatusCode.OK
            };
        }
    
        public string GeneratePreSignedURL(string bucketName,string key)
        {
            string urlString = "";
            try
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    Expires = DateTime.Now.AddMinutes(1)
                };
                urlString = _client.GetPreSignedURL(request1);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            return urlString;
        }
    }
}

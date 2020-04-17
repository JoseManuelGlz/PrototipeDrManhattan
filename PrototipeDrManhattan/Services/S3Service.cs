using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
//using NETCore.Encrypt;
using S3TestWebApi.Model;
using S3TestWebApi.RsaEncrypt;
using static PrototipeDrManhattan.Services.Utils;



namespace S3TestWebApi.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;

        //These keys are of 2048byte
        //private readonly static string PublicKey = "-----BEGIN PUBLIC KEY-----MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjuI2LkXN5P5YQO3Soek+7TkaOWcT0wGQCzZTh2Uupt3+bhXU31Asg0Qu6loL6AtD91Mdie7c10RijGA3E9jnEAoZSBWlvgZ4+F1vc5PfMOAqxbqcDYcATSj/1f9x6YiK/0qcuvazkqaQdApvGDrw9Ozu7Rw9XoAQrYP5t/vFoOt6B3kzJcka2gt0yfYe1AB2lQwu90s6zrWxq59MpDLthjESosPkWbMU+E8c5kA07qCsBWYQ9gEcAs+GSA+3o0MUhJn495k/Tt6FsA63UGY/GZpvk9Jjp9H5+nyJRXnLuCUwJa4d5koEcf7mbz1syJgKG+Juo1x7Df9ixywEiXH5sQIDAQAB-----END PUBLIC KEY-----";
        //private readonly static string PrivateKey = "-----BEGIN RSA PRIVATE KEY-----MIIEowIBAAKCAQEAjuI2LkXN5P5YQO3Soek+7TkaOWcT0wGQCzZTh2Uupt3+bhXU31Asg0Qu6loL6AtD91Mdie7c10RijGA3E9jnEAoZSBWlvgZ4+F1vc5PfMOAqxbqcDYcATSj/1f9x6YiK/0qcuvazkqaQdApvGDrw9Ozu7Rw9XoAQrYP5t/vFoOt6B3kzJcka2gt0yfYe1AB2lQwu90s6zrWxq59MpDLthjESosPkWbMU+E8c5kA07qCsBWYQ9gEcAs+GSA+3o0MUhJn495k/Tt6FsA63UGY/GZpvk9Jjp9H5+nyJRXnLuCUwJa4d5koEcf7mbz1syJgKG+Juo1x7Df9ixywEiXH5sQIDAQABAoIBAEyaevHT+s8KjcZBuuuTYWlNdAHSgY5fCBr6xU2RsbFE02Ox0K7pDPRTWdPK8xc08vtmVC/fIAdJYoxgCSda4oZ245cCBBoc3j5J2bbdUIujo0rfAUs/VaoIkSDqEuhhjCPSnLSgDyZQpqGP9n/HGeg2HuKAgWZowohEeV1qXkonI/hYfmTPDKSxe2N9Dcz9awcTNldz2iSOkJysTl+9xZh0YAxDwYoFF1adfa7a936+47dnA+9OG/JL8ooZqOdf5M7wAMJvWkcQTsApPQhpLPjfGmLwHRbNpxxHj/MJuj7N663/3zUlxGqhKz74KiyoTHOmV31D07/k/dFacpKi3QECgYEA6M1i928VXoQWFQ6Zr9vaYnv3W4zhlAYjITjAjT3qr4PCkTz55oE+sgdTxB500WjM7Fvd4Uc/y+nOtUzoGFEzXX/n8UDxH4/yrnzV7WcUJi8aarGQHGyjRwa99uQVf8TXloU2n5gWmZ9QirRiJelxESHffC+Rp4kGMaRVGM1Zqp8CgYEAnR8RK+EJpC1nYmB8tyxK+XxqpPDd3aEbRQ8alkbzkcgwE1h4wSA3hi2uua5vP24do8E8NoO5TcNhEpGuenmiC4tOIIa+TB1FNjSt2d7InZBqbcl35x9b8hBQqyJA/ntnZ0u/rIymLK9vP4SB6c8p0hHjn6hJEfoRdNH+D8chSa8CgYAmfB2KLfDoaQmFfZ5mdf/KHBguKEH6SHFyQoGCV+P+E9gMTno5YtBMee2dFDPXkm2d/SnW3gJr4UDss8QCkRnesRBUz5mM0C0cL0LmNg4cqdjCHoxmRodPbIvmzpnHP3EsTuVSyL+jsbSM2XkIL2kjnefmDa8UdRBzEGmI6hmSnQKBgAva+T+3VjPmHSPmJeLoW6vhgGZ7qjH74wPd9OKb7er1EPOUvF/OH4JnVgS6rbg6hdACV5nBbKz6bguppGGnZdu7IiUAJcG2f69sYCXNUpPY/r7T8dElo5lrM7sF9aN+CjpYE9lgL3W1sw424FqVABHSubslRoYtuT14TtQ/8ToPAoGBAL4ke5jLYrojzDQM6Hm+Y9Qj9ICFmEIWqgW9ZyWfZjZJmpnEMvFLCwBmF832utQxGOU/j1olzpkY8lA7cre1FmF/WeLHBwelGE/4ihyQRpCt8ErQTDZ6OmBXMEoXMyefno9J0nNzQjndgtAGvKwUfYd8vJK+iRzHhq2F0FFqdz85-----END RSA PRIVATE KEY-----";
        /*
        private readonly static string PublicKey = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA6OpeTeu2idA1lxUWi5CU
2AMCyMdtGRWmdvEOFKxi6YYeYKlUinenUXbBjDPk9jAQOuCu3z8gIcWqmhoyuLP4
SJll9yrYplEBW4Zm2cNX3f0myjT3GsSzl/W7QeoffbfXStsrRr5WpFZMvcusCHr4
e8f9TGM5TNt3f27iaVyQq2DVe12NAPlmpnmjZ4ZDj2pIhkJZWoSWmppiXfwVPS9b
kscgsZxrnu1qLWBb83Zf1fyuoRmkIXNyeskFGMVo/I5MBBwNAH5xzF8dtRI1x4EP
kU3kSy8NLQGB0uO/385LetFBnjJDN/eq8JZZg4p4C1UWZtppv0VLBejvVdlUGjbc
8QIDAQAB
-----END PUBLIC KEY-----";
        private readonly static string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEA6OpeTeu2idA1lxUWi5CU2AMCyMdtGRWmdvEOFKxi6YYeYKlU
inenUXbBjDPk9jAQOuCu3z8gIcWqmhoyuLP4SJll9yrYplEBW4Zm2cNX3f0myjT3
GsSzl/W7QeoffbfXStsrRr5WpFZMvcusCHr4e8f9TGM5TNt3f27iaVyQq2DVe12N
APlmpnmjZ4ZDj2pIhkJZWoSWmppiXfwVPS9bkscgsZxrnu1qLWBb83Zf1fyuoRmk
IXNyeskFGMVo/I5MBBwNAH5xzF8dtRI1x4EPkU3kSy8NLQGB0uO/385LetFBnjJD
N/eq8JZZg4p4C1UWZtppv0VLBejvVdlUGjbc8QIDAQABAoIBADjkRnSyBrxS50Qo
GoGspy68TYnNbJCcATU8XDwgf2ibIgBqXAQWhgAYCd69LkgUJWK8Tvqo6KhXJ/zM
OWd0TkktbwKYrKKzVqmMgsLePDA1DayGk3UuZs/PbhYDof2LUkMulbuJ8o/hpI0X
msXXg6p4hx6Jfg2P7fhqlVM3EP9LCG85TM/4183n1g/R+mq76mh48UELetZtH4V8
XJiHbVLJhbtKeggD1GofhNud5s9kwHeN+47yBbWZBRLLZ2WfV5HuZGgWNb9vsfdO
q/KXIRpUOoRNOSBmMKbMpJrqjpg+skYdGtfqkp54R/d8InysrCoiigGg2KZJPNXt
oiRRiSECgYEA/7zRzEL6ISyvxWHUqZVHkufsDkNm3Z29Z0FlwmsZnrfjrOzQW8cj
THuQcpIU2OlYDzflquLA04DHMCTqj5xBYOLg6xAQOO3LJpIXhx1X9tigKAviKPSd
pyS8xiiRtozcxqLK89glqEH8Kma+Elt/MQmnZJAcavQv5orgfILAEe8CgYEA6SeN
vD57gDfoQKqaSAXN7v3aCjHoLuoOpyTXSB+tgRLQoyfxt8h4rYcEw/kRp/mitLUY
CKV4FG2NwEs6QR2qfA+bwDhqrLBvGO195IFte7h3KDeAG2YS+1okO/5h6jfRQfk9
E2uA/IZBjKMxkCYP1mokPcsRbTJQ+PEveD+DXx8CgYBhcwUK7da8f3e3IhFUUaMF
csS7pqly92GuD/iDviX0GiRyx4aaAmcMBOXFEq9I/JnmqqkkleVecur0CI0tDiDH
l4yXZfaYitxnpTG79c2ILYHR4L1cV+IfR9t7MwhbZI/YTT5C7vFijUpWqfHxstXj
zMbxhgyeINCD7BWgvH4OFwKBgH9rRmJiI0xnQV5V3gsOYQUDZm276I/7Chb+Y0jf
RwsLJUqFQeyWep0a9NfE/ok2PF1VutS+2WkQli9I7YU4mTtDrHLYYjQOGCkfYXXH
5fV/Ul+ANVrD5gvHr5W55/kAmDPd6ir6zXs0RA6AAU4t/unHCBddKyDqJqZl6PNm
gn0xAoGBAOw6cJ10EVnbkbuADg2prBcoBvp0Gm9oPt1H6Wxj7hU2SAfIIsPqpbaK
bpf0YDecG4zv6mx+5G1Kx4XDVJM00ZElmLt+1i1isM3ElPSKzI+1RKn9UpSA84tM
u1rTgw3y/txOjTryzGQwJ4xnthyWjAjBaBYuH00S+uqNGOa4udLQ
-----END RSA PRIVATE KEY-----";
*/
        /*
                private readonly static string PublicKey = @"-----BEGIN PUBLIC KEY-----
        MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAOsftMC+o9T1PqKfTitdcFCzl+2iNrd8
        ExxkOXaCQsCEXO0VgXSxdLCgaDE3TAaCcqlLmO86FSkhubvM9ayeLLcCAwEAAQ==
        -----END PUBLIC KEY-----";
                private readonly static string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
        MIIBPAIBAAJBAOsftMC+o9T1PqKfTitdcFCzl+2iNrd8ExxkOXaCQsCEXO0VgXSx
        dLCgaDE3TAaCcqlLmO86FSkhubvM9ayeLLcCAwEAAQJAcllkLg1JXmu7f5mciciS
        tBzz+bVXiRsFrFwt+i3Vywxl4SUVGvnPOCdAFv75+4BcAi8aN+uAMQ+ki+Ubrf9E
        CQIhAP35fFBdhJQ6LV9Rkf1S5Rw7/4VB/2j4gnTbSPXXCuabAiEA7P+8IN+q7XFH
        9S99diJhJIMy/4Q4fr22H3XQnmw+5hUCIQCE7xQpgHmHmFftX2W5oaz4NVNObbgB
        OqoV/j1XKEK1ZwIhALCI+eh+0wKVPCV9n3XMvBGHjQhZw/9cbvnWN3SaauvVAiEA
        6q0T7bvO6CAQ19WUCvt9Y3h5YgPqUXZgW6X3fXuErL8=
        -----END RSA PRIVATE KEY-----";
        */
        private readonly static string PublicKey = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzc6D73iuwjoGqA8C3JQw
Le8/3JlOjmDJhYizSERyby8MKpE5Y4uoW8sp8rom+KkxyiS9H+w0axPZVo9nf+66
X9MQ1Te29jsFZVKZOV9Y907VD3PtI5Adz3YKfNdIv6OFoDYx9OQ7e6KkvGjQFBwp
xV0mMVqOw0MpnPDsKo3kU4GyY3d8caDM2+YjA3fPleZWXWhRZDzpc3Jh0mqZXtu+
eNJ/4pm+ry7DR7h/KIcXZ/9KvMhVb6EQC7vmOdZbhB4Sjvfc1fy39y9R5YzTBu+B
FfoFhjnONLGTeQBAXlg6C/mK6ZamRKSCUv20GgI38WZ0YasfW30X0l3ArmF4tJxk
twIDAQAB
-----END PUBLIC KEY-----";
        private readonly static string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAzc6D73iuwjoGqA8C3JQwLe8/3JlOjmDJhYizSERyby8MKpE5
Y4uoW8sp8rom+KkxyiS9H+w0axPZVo9nf+66X9MQ1Te29jsFZVKZOV9Y907VD3Pt
I5Adz3YKfNdIv6OFoDYx9OQ7e6KkvGjQFBwpxV0mMVqOw0MpnPDsKo3kU4GyY3d8
caDM2+YjA3fPleZWXWhRZDzpc3Jh0mqZXtu+eNJ/4pm+ry7DR7h/KIcXZ/9KvMhV
b6EQC7vmOdZbhB4Sjvfc1fy39y9R5YzTBu+BFfoFhjnONLGTeQBAXlg6C/mK6Zam
RKSCUv20GgI38WZ0YasfW30X0l3ArmF4tJxktwIDAQABAoIBAFfDKBn3gbaLngkO
la7Qdpcv/jCpI4mtlmIeQC2iGpZ1HqAMW8eqZ3n1cgbzAOlq5TOJZSj2xgefaD/0
WTOB7vIsBHKYFqp70ro5deO0WDDl6g9z5P0UWNH+SUyhVYF7TuFaGwWOShtX6R37
KDhbJijmpwHG1zhHLLijOGKRLXu6AAM5WbztpR+cVze/mC06bnhC6HogfQCMJjZL
nCXdMW4TZnUQwWBNIfyyqQvL9m9kLI2jvLFhpjgWQgoir5XIQk3x0KGiOBt8IRNp
8GrxZYItLuFgU4hkSL1/d6U87VedD3goi/5H2X2IMNI4WeDiJdfpHk5sbcwW2y7M
NMItQgECgYEA8RIX78cv+eoVZuUCVGKDyTGaJ8xCGce15SgrSB0G4+/4qorMEiov
K9fUYNGiz59U0FiWMQ+PHrUl6/V0G+c0uUAvt/hpoA/Vu1LzRxXF+rly2lvXKwqX
5huVgeCzaNC5ZTb90y3m2PUwDeFvcGNlUy1q2AZP8LrUPmKfRZRABbcCgYEA2o1Z
0x+a84Zqvy17vQSy9AilII+3sXp/s73AQu0fvqYCZiSh8sAPBhpbPXPRzVrGTqmN
4N9peNjwsvOOhbPU7hOdcWweJcQLXTc3sJfTE3EIBkpoTvBkitLlA1nGwnqEMD0w
ynPM/Rr95I+/1m3ASYSBAKp/MtSgBbFlhVqemQECgYAvspvH1op4kUdQx4kRdziK
C2Vr8G83uJsVzz/ZEd02Jln2LGY6Rdx1eUvNKE1ldSoL2ytEY8a2lbL+H9+sUa/N
45RNezoy8E8itEPsUbONazn9WGSXYI4zeku3meUFnR7BlwLb0N43GTQ72tn4y8HV
lkvomD8i62RpP4wx8SZFuwKBgHxCzgWZ/LHlhR/5rlcEKvNRTwG6dQj1y1HY202y
INB29vo4bdPlmyUvKx0/ktQdfo8PHFn07LUHM6Orkyc13iUXBfeNL37tfkCbupAv
YpW1OIjLGY94YtIDoq5LBxVgL3R19h3FxZFLHXwstzgl4qRqMCa+yd+OwQU7nas6
oN4BAoGBANlSWJOErlCNRNWqmlw7CeLxQ+QsDt8hk0wAjKr8UQLpBQqhXxrpGVok
u5zO7JPIeDWRqaBqRXhq1udg1B35F3AYVy+ICcJEZXabiqSA3l0r8aZunhJqfjfV
4uYoHNSkzvkF5AuWbKqDFNspE46OhygmDM0xag445ztYUlb4eVsq
-----END RSA PRIVATE KEY-----";

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                var exist = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);
                if (!exist)
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
                    Message = ""
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

        public async Task<S3Response> DeleteBucketAsync(string bucketName)
        {
            try
            {
                var exist = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);
                if (exist)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };

                    var response = await _client.DeleteBucketAsync(bucketName);

                    return new S3Response
                    {
                        Status = response.HttpStatusCode
                    };
                }
                return new S3Response
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = ""
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

                lstRequestObject.BucketName = bucketName;

                var lstObject = await _client.ListObjectsV2Async(lstRequestObject);

                var lstResponse = lstObject.S3Objects.Select(x => new S3ObjectResponse(x.BucketName, x.ETag, x.Key, x.LastModified, x.Size, x.StorageClass));

                return new S3LstObjectResponse(lstObject.HttpStatusCode, lstResponse);
            }
            catch (AmazonS3Exception ex)
            {
                return new S3LstObjectResponse(ex.StatusCode, null);
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

        public async Task<S3Response> UploadFile2Async(Stream stream, string bucketName, string fileName)
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = "application/pdf",
                    CannedACL = S3CannedACL.PublicRead
                };

                var response = await _client.PutObjectAsync(request).ConfigureAwait(false);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new S3Response
                    {
                        Message = "File uploaded Successfully",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new S3Response
                    {
                        Message = response.ToString(),
                        Status = HttpStatusCode.InternalServerError
                    };
                }
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


        }

        public string GeneratePreSignedURL(string bucketName, string key)
        {
            string urlString = "";
            try
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    Expires = DateTime.Now.AddMinutes(1)
                    //,ContentType= "application/octet-stream"
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

        private bool IsPDFHeader(Stream file)
        {
            byte[] buffer = null;

            BinaryReader br = new BinaryReader(file);
            buffer = br.ReadBytes(5);

            var enc = new ASCIIEncoding();
            var header = enc.GetString(buffer);

            //%PDF−1.0
            // If you are loading it into a long, this is (0x04034b50).
            if (buffer[0] == 0x25 && buffer[1] == 0x50
                && buffer[2] == 0x44 && buffer[3] == 0x46)
            {
                return header.StartsWith("%PDF-", StringComparison.Ordinal);
            }
            return false;

        }

        private ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }

        public string validateFile(string type, Stream file)
        {
            switch (type)
            {
                case "application/pdf":
                    bool isPdf = IsPDFHeader(file);

                    if (isPdf)
                    {
                        return "pdf";
                    }

                    return "Pdf no valido";

                    break;
                case "image/jpeg":
                case "image/png":
                    byte[] b;

                    using (BinaryReader br = new BinaryReader(file))
                    {
                        b = br.ReadBytes((int)file.Length);
                    }

                    var r = GetImageFormat(b);

                    if (r == ImageFormat.unknown)
                    {
                        return "Imagen no valida";
                    }
                    else
                    {
                        return r.ToString();
                    }

                    break;
                case "application/msword":
                    bool isdoc = IsDocFile(file);

                    if (isdoc)
                    {
                        return "doc";
                    }

                    return "doc no valido";

                    break;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    bool isdocx = IsDocFile(file);

                    if (isdocx)
                    {
                        return "docx";
                    }

                    return "docx no valido";

                    break;

            }
            return "No es un archivo valido";
        }

        private bool IsDocFile(Stream file)
        {
            bool isDocFile = false;
            //
            // File sigs from: http://www.garykessler.net/library/file_sigs.html
            //
            string msOfficeHeader = "D0-CF-11-E0-A1-B1-1A-E1";
            string docSubHeader = "EC-A5-C1-00";

            //get file header
            byte[] headerBuffer = new byte[8];
            file.Read(headerBuffer, 0, headerBuffer.Length);
            string headerString = BitConverter.ToString(headerBuffer);

            if (headerString.Equals(msOfficeHeader, StringComparison.InvariantCultureIgnoreCase))
            {
                //get subheader
                byte[] subHeaderBuffer = new byte[4];
                file.Seek(512, SeekOrigin.Begin);
                file.Read(subHeaderBuffer, 0, subHeaderBuffer.Length);
                string subHeaderString = BitConverter.ToString(subHeaderBuffer);

                if (subHeaderString.Equals(docSubHeader, StringComparison.InvariantCultureIgnoreCase))
                {
                    isDocFile = true;
                }
            }

            return isDocFile;
        }

        private bool IsDocxFile(Stream file)
        {
            bool isDocFile = false;
            //
            // File sigs from: http://www.garykessler.net/library/file_sigs.html
            //
            string msOfficeHeader = "50-4B-03-04-14-00-08-08";
            string msOfficeHeadeR = "50-4B-03-04-14-00-06-00";
            string docSubHeader = "EC-A5-C1-00";

            //get file header
            byte[] headerBuffer = new byte[8];
            file.Read(headerBuffer, 0, headerBuffer.Length);
            string headerString = BitConverter.ToString(headerBuffer);

            if (headerString.Equals(msOfficeHeader, StringComparison.InvariantCultureIgnoreCase))
            {
                //get subheader
                byte[] subHeaderBuffer = new byte[4];
                file.Seek(512, SeekOrigin.Begin);
                file.Read(subHeaderBuffer, 0, subHeaderBuffer.Length);
                string subHeaderString = BitConverter.ToString(subHeaderBuffer);

                if (subHeaderString.Equals(docSubHeader, StringComparison.InvariantCultureIgnoreCase))
                {
                    isDocFile = true;
                }
            }

            return isDocFile;
        }

        public async Task<byte[]> DownloadFile(string url)
        {
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(url))
                {
                    result.Content.IsMimeMultipartContent("application/pdf");
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                }
            }
            return null;
        }

        public async Task<Stream> DownloadFile2(string url)
        {
            var wc = new System.Net.WebClient();
            wc.DownloadFile(url, System.IO.Directory.GetCurrentDirectory() + "/testm8.pdf");

            return new FileStream(System.IO.Directory.GetCurrentDirectory() + "/testm8.pdf", FileMode.Open);
        }

        public async Task<S3Response> DeleteObjectAsync(string bucketName, string fileName)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                var response = await _client.DeleteObjectAsync(request).ConfigureAwait(false);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new S3Response
                    {
                        Message = "File Delete Successfully",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new S3Response
                    {
                        Message = response.ToString(),
                        Status = HttpStatusCode.InternalServerError
                    };
                }
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

        public async Task<S3Response> CreateFolderAsync(string bucketName, string folderName)
        {
            try
            {

                var folderKey = folderName + "/"; //end the folder name with "/"

                var request = new PutObjectRequest();

                request.BucketName = bucketName;

                request.StorageClass = S3StorageClass.Standard;
                request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.None;

                //request.CannedACL = S3CannedACL.BucketOwnerFullControl;

                request.Key = folderKey;

                request.ContentBody = string.Empty;

                var response = await _client.PutObjectAsync(request).ConfigureAwait(false);

                return new S3Response
                {
                    Message = response.ResponseMetadata.RequestId,
                    Status = response.HttpStatusCode
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

        public async Task<GetObjectMetadataResponse> DoesFolderExist(string bucketName, string folder)
        {
            try
            {
                var response = await _client.GetObjectMetadataAsync(new GetObjectMetadataRequest()
                {
                    BucketName = bucketName,
                    Key = folder
                }).ConfigureAwait(false);

                return response;
            }
            catch (Amazon.S3.AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    //return false;

                    //status wasn't not found, so throw the exception
                    throw;
            }
            return null;
        }

        public async Task<S3LstObjectResponse> listObjectFromFolder(string bucketName, string folder)
        {
            try
            {
                var lstRequestObject = new ListObjectsV2Request();

                lstRequestObject.BucketName = bucketName;
                lstRequestObject.Prefix = folder + "/";
                lstRequestObject.MaxKeys = 1;

                var lstObject = await _client.ListObjectsV2Async(lstRequestObject);

                var lstResponse = lstObject.S3Objects.Select(x => new S3ObjectResponse(x.BucketName, x.ETag, x.Key, x.LastModified, x.Size, x.StorageClass));

                return new S3LstObjectResponse(lstObject.HttpStatusCode, lstResponse);
            }
            catch (AmazonS3Exception ex)
            {
                return new S3LstObjectResponse(ex.StatusCode, null);
            }
        }

        public async Task<S3Response> UploadFile2Async(Stream stream, string bucketName, string folderName, string fileName)
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = folderName + "/" + fileName,
                    InputStream = stream,
                    ContentType = "application/pdf",
                    CannedACL = S3CannedACL.Private
                };

                var response = await _client.PutObjectAsync(request).ConfigureAwait(false);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new S3Response
                    {
                        Message = "File uploaded Successfully",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new S3Response
                    {
                        Message = response.ToString(),
                        Status = HttpStatusCode.InternalServerError
                    };
                }
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


        }

        public string EncryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public string DecryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        #region RSA for net framework complete
        /*
        public string EncryptRSA(string plainText)
        {
            int keySize = 0;
            string publicKeyXml = "";

            GetKeyFromEncryptionString(PublicKey, out keySize, out publicKeyXml);

            var encrypted = Encrypt(Encoding.UTF8.GetBytes(plainText), keySize, publicKeyXml);

            return Convert.ToBase64String(encrypted);
        }

        private byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            int maxLength = GetMaxDataLength(keySize);
            if (data.Length > maxLength) throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }

        private int GetMaxDataLength(int keySize)
        {
            if (_optimalAsymmetricEncryptionPadding)
            {
                return ((keySize - 384) / 8) + 7;
            }
            return ((keySize - 384) / 8) + 37;
        }

        private bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 && keySize <= 16384 && keySize % 8 == 0;
        }

        private void GetKeyFromEncryptionString(string rawkey, out int keySize, out string xmlKey)
        {
            keySize = 0;
            xmlKey = "";

            if (rawkey != null && rawkey.Length > 0)
            {
                byte[] keyBytes = Convert.FromBase64String(rawkey);
                var stringKey = Encoding.UTF8.GetString(keyBytes);

                if (stringKey.Contains("!"))
                {
                    var splittedValues = stringKey.Split(new char[] { '!' }, 2);

                    try
                    {
                        keySize = int.Parse(splittedValues[0]);
                        xmlKey = splittedValues[1];
                    }
                    catch (Exception e) { }
                }
            }
        }

        public string DecryptRSA(string encryptedText)
        {
            int keySize = 0;
            string publicAndPrivateKeyXml = "";

            GetKeyFromEncryptionString(PrivateKey, out keySize, out publicAndPrivateKeyXml);

            var decrypted = Decrypt(Convert.FromBase64String(encryptedText), keySize, publicAndPrivateKeyXml);

            return Encoding.UTF8.GetString(decrypted);
        }

        private byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicAndPrivateKeyXml)) throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }
        */
        #endregion

        public string EncryptRSA(string plainText)
        {
            /*
            var rsaKey = EncryptProvider.CreateRsaKey();
            var publicKey = rsaKey.PublicKey;
            var privateKey = rsaKey.PrivateKey;

            var encrypted = EncryptProvider.RSAEncrypt(publicKey, plainText);

            encrypted = EncryptProvider.RSAEncrypt(publicKey, plainText, RSAEncryptionPadding.Pkcs1);


            var decrypted = EncryptProvider.RSADecrypt(privateKey, encrypted, RSAEncryptionPadding.Pkcs1);

            return publicKey + "  = " + decrypted;*/
            
            var encrypted = EncryptProvider.RSAEncryptWithPem(PublicKey, plainText);
            var decrypted = EncryptProvider.RSADecryptWithPem(PrivateKey, encrypted);
            return decrypted + " -- " + encrypted;
            

            
            //return "";
        }

        public string DecryptRSA(string encryptedText)
        {
            return EncryptProvider.RSADecryptWithPem(PrivateKey, encryptedText);

        }


        public async Task<S3Response> renameFile(string url)
        {
            //

            var objectUrl = new AmazonS3Uri(url);
            string bucketName=objectUrl.Bucket;
            string keyName = objectUrl.Key;
            RegionEndpoint regionName = objectUrl.Region;



            var file = new CopyObjectRequest();
            file.SourceBucket = bucketName; 
            file.SourceKey = keyName;

            file.DestinationBucket = bucketName;
            file.DestinationKey = "testmonboarding/" + "476843f1-b357-4bb1-b181-1dcc3b6a45f5";
            file.CannedACL = S3CannedACL.Private;

            var response = await _client.CopyObjectAsync(file).ConfigureAwait(false);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return new S3Response
                {
                    Message = "File uploaded Successfully",
                    Status = HttpStatusCode.OK
                };
            }
            else
            {
                return new S3Response
                {
                    Message = response.ToString(),
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

    }
}

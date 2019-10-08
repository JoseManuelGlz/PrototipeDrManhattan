using System.IO;
using System.Threading.Tasks;
using S3TestWebApi.Model;

namespace S3TestWebApi.Services
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);

        Task<S3ListBucketResponse> listBuckets();

        Task<S3LstObjectResponse> listObjectFromBucket(string bucketName);

        Task<S3Response> UploadFileAsync(Stream fileToUpload, string bucketName);

        string GeneratePreSignedURL(string bucketName, string key);

        string validateFile(string type, Stream file);
    }
}

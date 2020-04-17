using System.IO;
using System.Threading.Tasks;
using Amazon.S3.Model;
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

        Task<byte[]> DownloadFile(string url);

        Task<Stream> DownloadFile2(string url);

        Task<S3Response> UploadFile2Async(Stream stream, string bucketName, string fileName);

        Task<S3Response> DeleteObjectAsync(string bucketName, string fileName);

        Task<S3Response> DeleteBucketAsync(string bucketName);

        Task<GetObjectMetadataResponse> DoesFolderExist(string bucketName, string folder);

        Task<S3Response> CreateFolderAsync(string bucketName, string folderName);

        Task<S3LstObjectResponse> listObjectFromFolder(string bucketName, string folder);

        Task<S3Response> UploadFile2Async(Stream stream, string bucketName, string folderName, string fileName);

        string EncryptString(string text, string keyString);

        string DecryptString(string cipherText, string keyString);

        string EncryptRSA(string plainText);

        string DecryptRSA(string encryptedText);

        Task<S3Response> renameFile(string url);
    }
}

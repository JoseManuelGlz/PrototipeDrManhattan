using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3TestWebApi.Services;

namespace S3TestWebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/S3Buckt")]
    public class S3BucketController : Controller
    {
        private readonly IS3Service _service;

        public S3BucketController(IS3Service service)
        {
            _service = service;
        }

        [HttpPost("{bucketName}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketName)
        {
            var response = await _service.CreateBucketAsync(bucketName);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> listBuckets()
        {
            var result = await _service.listBuckets();
            return Ok(result.buckets);
        }

        [HttpGet("{bucketName}/objects")]
        public async Task<IActionResult> lstObjectFromBucket([FromRoute] string bucketName)
        {
            var response = await _service.listObjectFromBucket(bucketName);
            return Ok(response);
        }

        [HttpPost("{bucketName}/uploadfile")]
        public async Task<IActionResult> uploadfile([FromRoute] string bucketName, [FromForm] IFormFile file)
        //public  IActionResult uploadfile([FromRoute] string bucketName, [FromForm] IFormFile file)
        {
            if (file != null)
                {
                    
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        var s=reader.BaseStream;
                        var response = await _service.UploadFileAsync(s,bucketName);
                        return Ok(response);
                    }
                }
            return Ok();
        }

        [HttpGet("{bucketName}/files/{fileName}")]
        public IActionResult getUrlFile([FromRoute] string bucketName, [FromRoute] string fileName)
        {
            var response = _service.GeneratePreSignedURL(bucketName,bucketName);
            return Ok(response);
        }
    }
}

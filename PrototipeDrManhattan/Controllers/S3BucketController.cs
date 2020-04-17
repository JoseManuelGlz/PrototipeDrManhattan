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
                    var s = reader.BaseStream;
                    //var response = await _service.UploadFileAsync(s,bucketName);
                    var responseAmazon = await _service.UploadFile2Async(s, "testmonboarding", "testmonboarding", "6eda2909-1116-4e4a-abaf-175fdef2477d").ConfigureAwait(false);

                    return Ok(responseAmazon);
                }
            }
            return Ok();
        }

        [HttpGet("{bucketName}/files/{fileName}")]
        public IActionResult getUrlFile([FromRoute] string bucketName, [FromRoute] string fileName)
        {
            var response = _service.GeneratePreSignedURL(bucketName, fileName);
            return Ok(response);
        }

        [HttpGet("{bucketName}/filesUrl")]
        public IActionResult getUrlFiles([FromRoute] string bucketName, [FromBody] string fileName)
        {
            var response = _service.GeneratePreSignedURL(bucketName, fileName);
            return Ok(response);
        }

        [HttpGet("validateFile")]
        public IActionResult validateFile([FromForm] IFormFile file)
        {
            if (file != null)
            {

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var s = reader.BaseStream;
                    var response = _service.validateFile(file.ContentType, s);
                    return Ok(response);
                }
            }
            return Ok();
        }

        [HttpPost("soraContrato")]
        //public async Task<IActionResult> sora([FromForm] IFormFile file)
        public async Task<IActionResult> sora([FromBody]string url)
        {
            /*
            var response = await _service.DownloadFile(@"https://sora-contratos.s3.us-east-2.amazonaws.com/contratos/final-signed-2019-09-19T02%3A35%3A34.442Z.pdf").ConfigureAwait(false);

            Stream stream = new MemoryStream(response);

            

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var s = reader.BaseStream;

                using (var reader1 = new StreamReader(stream))
                {
                    var s1 = reader1.BaseStream;

                    if (s1 == s)
                    {
                        return Ok();
                    }
                }

            }
        */
            /*
            Stream s1 = new MemoryStream();

            using (var writer = new BinaryWriter(s1))
            {
                writer.Write(response);
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var s = reader.BaseStream;
                    if (stream == s1)
                    {
                        return Ok();
                    }
                }
            }
            */

            //using (BinaryReader br = new BinaryReader(stream))
            //{
            //var DocDataStream = br.ReadBytes((int)stream.Length);
            //using (var reader = new StreamReader(new MemoryStream(DocDataStream)))
            //{
            /*
            var isValid = _service.validateFile("application/pdf", stream);

            if (isValid == "pdf")
            {
                var responseAmazon = await _service.UploadFileAsync(stream, "testm8");
                return Ok(responseAmazon);
            }
            */
            //return Ok();
            //}
            //}
            /*
            var myfile = new FileStreamResult(stream, "application/pdf");

            var msFile = new MemoryStream();
            myfile.FileDownloadName = "testm8.pdf";
            myfile.FileStream.CopyTo(msFile);
            msFile.Position = 0;
            */



            //return Ok(response);

            /*
            var response = await _service.DownloadFile2(url).ConfigureAwait(false);
            using (var reader = new StreamReader(response))
            {
                var s = reader.BaseStream;
                
                var responseAmazon = await _service.UploadFileAsync(s, "testm8");
                return Ok(responseAmazon);
            }
              */

            var response = await _service.DownloadFile2(url).ConfigureAwait(false);

            var responseAmazon = await _service.UploadFile2Async(response, "testmonboarding", "myfolder", "testm8").ConfigureAwait(false);

            //var responseAmazon = await _service.UploadFileAsync(response, "testm8");

            return Ok(responseAmazon);

        }

        [HttpDelete("{bucketName}/files/{fileName}")]
        public IActionResult deleteFile([FromRoute] string bucketName, [FromRoute] string fileName)
        {
            var response = _service.DeleteObjectAsync(bucketName, fileName);
            return Ok(response);
        }

        [HttpGet("{bucketName}/folder/{folder}")]
        public async Task<IActionResult> FolderExist([FromRoute] string bucketName, [FromRoute] string folder)
        {
            var response = await _service.DoesFolderExist(bucketName, folder).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPost("{bucketName}/folder/{folderName}")]
        public async Task<IActionResult> CreateFolder([FromRoute] string bucketName, [FromRoute] string folderName)
        {
            var response = await _service.CreateFolderAsync(bucketName, folderName);
            return Ok(response);
        }

        [HttpGet("{bucketName}/objectsFolder/{folderName}")]
        public async Task<IActionResult> lstObjectFromBucket([FromRoute] string bucketName, [FromRoute] string folderName)
        {
            var response = await _service.listObjectFromFolder(bucketName, folderName);
            return Ok(response);
        }

        [HttpPost("cifrar/{cifrar}")]
        public ActionResult cifrado([FromRoute] string cifrar)
        {
            var response = _service.EncryptString(cifrar, "E546C8DF278CD5931069B522E695D4F2");
            return Ok(response);
        }

        [HttpPost("descifrar")]
        public ActionResult descifrado([FromBody] string cifrar)
        {
            var response = _service.DecryptString(cifrar, "E546C8DF278CD5931069B522E695D4F2");
            return Ok(response);
        }

        [HttpPost("cifrarRSA/{cifrar}")]
        public ActionResult cifradoRSA([FromRoute] string cifrar)
        {
            var response = _service.EncryptRSA(cifrar);
            return Ok(response);
        }

        [HttpPost("descifrarRSA")]
        public ActionResult descifradoRSA([FromBody] string cifrar)
        {
            var response = _service.DecryptRSA(cifrar);
            return Ok(response);
        }


        [HttpPost("migracion")]
        public async Task<IActionResult> migracion([FromBody]string url)
        {
            var response = await _service.renameFile(url);
            return Ok(response);
        }
    }
}

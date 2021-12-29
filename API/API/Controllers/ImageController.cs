using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IAmazonS3 _amazonS3;
        public ImageController(IAmazonS3 amazonS3)
        {
            this._amazonS3 = amazonS3;
        }
        [HttpPost]
        public async Task<IActionResult> SaveImage([FromForm] IFormFile file)
        {
            var putRequest = new PutObjectRequest()
            {
                BucketName = "apidisney546",
                Key = file.FileName,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            };
            var result = await this._amazonS3.PutObjectAsync(putRequest);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetImage(string file)
        {
            var request = new GetObjectRequest()
            {
                BucketName = "apidisney546",
                Key = file
            };
            using GetObjectResponse response = await this._amazonS3.GetObjectAsync(request);
            using Stream responseStream = response.ResponseStream;
            var stream = new MemoryStream();
            await responseStream.CopyToAsync(stream);   
            stream.Position = 0;

            return new FileStreamResult(stream, response.Headers["Content-Type"])
            {
                FileDownloadName = file
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ThumbnailGenerator.API.Services;

namespace ThumbnailGenerator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThumbnailController : ControllerBase
    {
        private readonly IThumbnailService _thumbnailService;

        public ThumbnailController(IThumbnailService thumbnailService)
        {
            _thumbnailService = thumbnailService;
        }

        [HttpPost("resize")]
        public async Task<IActionResult> Resize([FromForm(Name = "Data")] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not provided");

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var imageData = ms.ToArray();
                var resizedImage = _thumbnailService.ResizeImageAsync(imageData);
                return File(resizedImage, "image/jpeg"); 
            }
        }
    }
}

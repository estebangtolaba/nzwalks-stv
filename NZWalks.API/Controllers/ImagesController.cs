using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public IImageRepository ImageRepository { get; }

        public ImagesController(IImageRepository imageRepository)
        {
            ImageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // convert DTO to Domain model

                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await ImageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] {".jpg" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","Unsupported file extension.");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Too big.");

            }
        }
    }
}

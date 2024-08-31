using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			this.imageRepository = imageRepository;
		}

		[HttpPost]
		[Route("Upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
		{
			ValidateFileUploaded(requestDto);

			if (ModelState.IsValid)
			{
				var imagedomainModel = new Image
				{
					File = requestDto.File,
					FileDescriptioin = requestDto.FileDescriptioin,
					FileExtension = Path.GetExtension(requestDto.File.FileName),
					FileSizeInBytes = requestDto.File.Length,
					FileName = requestDto.FileName

				};

				await imageRepository.Upload(imagedomainModel);

				return Ok(imagedomainModel);
			}
			return BadRequest(ModelState);
		}
		private void ValidateFileUploaded(ImageUploadRequestDto requestDto)
		{
			var allowdExtensions = new string[] { "jpg", "png", "jpeg" };
			if (!allowdExtensions.Contains(Path.GetExtension(requestDto.File.FileName)))
			{
				ModelState.AddModelError("file", "Unsupported file extensions");
;			}
			if (requestDto.File.Length > 10485760)
			{
				ModelState.AddModelError("file", "your file more than 10MB! ");
			}
		}
	}
}

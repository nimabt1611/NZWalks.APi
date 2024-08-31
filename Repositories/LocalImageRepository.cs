using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly HttpContextAccessor httpContextAccessor;
		private readonly NZWalksDbContext dbcontext;

		public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
			HttpContextAccessor httpContextAccessor, NZWalksDbContext dbcontext)
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.dbcontext = dbcontext;
		}

		public async Task<Image> Upload(Image image)
		{
			var localfilepath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
				$"{image.FileName}{image.FileExtension}");


			//upload image to local path
			using var stream = new FileStream(localfilepath, FileMode.Create);
			await image.File.CopyToAsync(stream);

			var UrlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Image/{image.FileName}{image.FileExtension}";

			image.FilePath = UrlFilePath;


			//add image to table
			await dbcontext.Images.AddAsync(image);
			await dbcontext.SaveChangesAsync();
			return image;

		}
	}
}

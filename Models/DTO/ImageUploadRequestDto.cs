﻿namespace NZWalks.API.Models.DTO
{
	public class ImageUploadRequestDto
	{
		public IFormFile File { get; set; }
		public string FileName { get; set; }
		public string? FileDescriptioin { get; set; }
	}
}

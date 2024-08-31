using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class AddRegionRequestDto
	{
		[Requaired]
		[MaxLength(3 , ErrorMessage = "Code has be a Maxmum of 3 char")]
		[MinLength(3 , ErrorMessage = "Code has be a Minimum of 3 char")]
		public string Code { get; set; }
		[Requaired]
		[MaxLength(100, ErrorMessage = "Name has be a Maxmum of 3 char")]
		public string Name { get; set; }
		public string? RegionIMageUrl { get; set; }
	}
}

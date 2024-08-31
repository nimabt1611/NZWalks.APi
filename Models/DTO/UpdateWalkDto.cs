using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class UpdateWalkDto
	{
		[Requaired]
		[MaxLength(100)]
		public string Name { get; set; }
		[Requaired]
		[MaxLength(1000)]
		public string Discription { get; set; }
		[Requaired]
		[Range(0, 50)]
		public string LengtjKm { get; set; }
		[Requaired]
		public string WalkImageUrl { get; set; }
		[Requaired]
		public Guid DifficultyId { get; set; }
		[Requaired]
		public Guid RegionId { get; set; }
	}
}

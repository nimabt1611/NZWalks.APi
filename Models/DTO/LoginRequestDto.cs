using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class LoginRequestDto
	{
        [Requaired]
        [DataType(DataType.EmailAddress)]
        public string  Username { get; set; }
        [Requaired]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
    }
}

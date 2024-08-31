using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWallks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAllStudents()
		{
			string[] Student = new string[] { "nima", "javad", "Hossin" ,"Morteza"};
			return Ok(Student);
		}
	}
}

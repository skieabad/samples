using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Sample.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlobController : ControllerBase
    {
		readonly IWebHostEnvironment env;
		public BlobController(IWebHostEnvironment env) => this.env = env;


		[HttpGet("download/{fileName}")]
		public IActionResult Download(string fileName)
		{
			var path = Path.Combine(this.env.ContentRootPath, fileName);
			return this.PhysicalFile(path, "application/octet-stream");
		}


		[HttpPost("upload")]
		public async Task<ActionResult> Upload(IFormFile file)
        {
			var savePath = Path.Combine(this.env.WebRootPath, file.FileName);
			using (var stream = file.OpenReadStream())
				using (var fs = new FileStream(savePath, FileMode.CreateNew))
					await stream.CopyToAsync(fs);

			return this.Ok();
        }
	}
}

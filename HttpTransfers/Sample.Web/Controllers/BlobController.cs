using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Sample.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlobController : ControllerBase
    {
		readonly IWebHostEnvironment env;
		readonly ILogger logger;


		public BlobController(IWebHostEnvironment env, ILogger<BlobController> logger)
		{
			this.env = env;
			this.logger = logger;
		}


		[HttpGet("~/download/{fileName}")]
		public IActionResult Download(string fileName)
		{
			var path = Path.Combine(this.env.ContentRootPath, fileName);

			this.logger.LogInformation("Received download request for " + path);
			if (!System.IO.File.Exists(path))
            {
				this.logger.LogError($"File '{path}' does not exist");
				return this.NotFound();
            }
			return this.PhysicalFile(path, "application/octet-stream");
		}


		[HttpPost("~/upload")]
		public async Task<ActionResult> Upload(IFormFile file)
        {
			var msg = file == null ? "NO FILE" : $"File: {file.FileName} - Length: {file.Length}";
			this.logger.LogInformation(msg);

			var savePath = Path.Combine(this.env.WebRootPath, file.FileName);
			using (var stream = file.OpenReadStream())
				using (var fs = new FileStream(savePath, FileMode.CreateNew))
					await stream.CopyToAsync(fs);

			return this.Ok();
        }
	}
}

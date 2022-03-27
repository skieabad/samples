namespace PushApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PushApi.Infrastructure;


[ApiController]
[Route("[controller]")]
public class AzureController : ControllerBase
{
    readonly IOptions<PushConfig> options;
    public AzureController(IOptions<PushConfig> options) => this.options = options;
}

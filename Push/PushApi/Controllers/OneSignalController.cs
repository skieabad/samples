namespace PushApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PushApi.Infrastructure;


[ApiController]
[Route("[controller]")]
public class OneSignalController : ControllerBase
{
    readonly IOptions<PushConfig> options;
    public OneSignalController(IOptions<PushConfig> options) => this.options = options;
}

namespace PushApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Shiny.Extensions.Push;


[ApiController]
[Route("[controller]")]
public class ShinyController : ControllerBase
{
    readonly IPushManager pushManager;
    public ShinyController(IPushManager pushManager)
    {
        this.pushManager = pushManager;
    }


    [HttpPost]
    public async Task<ActionResult> Register()
    {
        await this.pushManager.UnRegister(PushPlatforms.Apple, "");
        return this.Ok();
    }


    [HttpPost]
    public async Task<ActionResult> UnRegister()
    {
        await this.pushManager.UnRegister(PushPlatforms.Apple, "");
        return this.Ok();
    }


    [HttpPost]
    public async Task<ActionResult> Send()
    {
        await this.pushManager.Send(
            new Notification(),
            new PushFilter()
        );
        return this.Ok();
    }
}

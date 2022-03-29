namespace PushApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Options;
using PushApi.Contracts;
using PushApi.Infrastructure;


[ApiController]
[Route("[controller]")]
public class AzureController : ControllerBase
{
    readonly NotificationHubClient hub;
    

    public AzureController(IOptions<PushConfig> options) 
    {
        var cfg = options.Value.AzureNotificationHubs ?? throw new InvalidOperationException("Azure Notifications Hub not configured");
        this.hub = NotificationHubClient.CreateClientFromConnectionString(cfg.FullConnectionString, cfg.HubName);
    }


    [HttpPost]
    public async Task<ActionResult> Send([FromBody] AzureSendArgs args)
    {
        //case "apns":
        //    // iOS
        //    var alert = "{\"aps\":{\"alert\":\"" + "From " + user + ": " + message + "\"}}";
        //outcome = await Notifications.Instance.Hub.SendAppleNativeNotificationAsync(alert, userTag);
        //break;
        //case "fcm":
        //    // Android
        //    var notif = "{ \"data\" : {\"message\":\"" + "From " + user + ": " + message + "\"}}";
        //outcome = await Notifications.Instance.Hub.SendFcmNativeNotificationAsync(notif, userTag);
        //break;
        return this.Ok();
    }
}

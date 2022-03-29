using Shiny.Extensions.Push;

namespace PushApi.Infrastructure;

public class PushConfig
{
    public OneSignalConfig OneSignal { get; set; }
    public AzureNotificationHubConfig AzureNotificationHubs { get; set; }

    public GoogleConfiguration Google { get; set; }
    public AppleConfiguration Apple { get; set; }
}

public class OneSignalConfig
{
    public string ApiKey { get; set; }
    //public string AppId { get; set; }
}

public class AzureNotificationHubConfig
{
    public string FullConnectionString { get; set; }
    public string ListenerConnectionString { get; set; }
    public string HubName { get; set; }
}
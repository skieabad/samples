namespace PushApi.Contracts;

using Shiny.Extensions.Push;


public class SendArgs
{
    public string Title { get; set; }
    public string Message { get; set; }
    public PushFilter? Filter { get; set; }
}

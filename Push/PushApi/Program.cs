using PushApi;
using PushApi.Infrastructure;
using Shiny.Extensions.Push;


var builder = WebApplication.CreateBuilder(args);

var pushConfig = builder.Configuration.Get<PushConfig>(); // ?? throw new InvalidProgramException("Could not set pushconfig");

builder.Services.AddPushManagement(x => x
    .AddGooglePush(pushConfig.Google ?? throw new InvalidOperationException("Google configuration not found"))
    .AddApplePush(pushConfig.Apple ?? throw new InvalidOperationException("Apple configuration not found"))
    .UseRepository<FilePushRepository>()
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<PushConfig>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

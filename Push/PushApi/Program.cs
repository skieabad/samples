using Microsoft.OpenApi.Models;

using PushApi;
using PushApi.Infrastructure;
using Shiny.Extensions.Push;


var builder = WebApplication.CreateBuilder(args);

#if DEBUG
var cfg = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

#else
var cfg = builder.Configuration;
#endif

var pushConfig = cfg.Get<PushConfig>() ?? throw new InvalidProgramException("Could not set pushconfig");

builder.Services.AddPushManagement(x => x
    .AddGooglePush(pushConfig.Google ?? throw new InvalidOperationException("Google configuration not found"))
    .AddApplePush(pushConfig.Apple ?? throw new InvalidOperationException("Apple configuration not found"))
    .UseRepository<FilePushRepository>()
);
builder.Services.Configure<PushConfig>(cfg);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x
    .SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Push Sample"
    })
);
builder.Services.Configure<PushConfig>(builder.Configuration);

var app = builder.Build();


app.UseSwaggerUI(x => 
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Push Sample v1")
);
app.UseSwagger();
app.MapControllers();

app.Run();

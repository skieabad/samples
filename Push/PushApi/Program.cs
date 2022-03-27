using PushApi;
using PushApi.Infrastructure;
using Shiny.Extensions.Push;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPushManagement(x => x
    .AddGooglePush(builder.Configuration.GetSection("Google").Get<GoogleConfiguration>() ?? throw new InvalidOperationException("Google configuration not found"))
    .AddApplePush(builder.Configuration.GetSection("Apple").Get<AppleConfiguration>() ?? throw new InvalidOperationException("Apple configuration not found"))
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

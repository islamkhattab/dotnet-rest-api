using System.Diagnostics;
using GameShop.Api.Authorization;
using GameShop.Api.Data;
using GameShop.Api.Endpoints;
//IF WE WANT TO USE CUSTOM ERROR HANDLER
//using GameShop.Api.ErrorHandling;
using GameShop.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameShopAuthorization();
builder.Services.AddHttpLogging(options => { });
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions.Add("Host", Environment.MachineName);
        ctx.ProblemDetails.Extensions.Add("TraceId", Activity.Current?.TraceId.ToString());
    };
});

var app = builder.Build();

//IF WE WANT TO USE CUSTOM ERROR HANDLER
//app.UseExceptionHandler( app => app.ConfigureExceptionHandler());

app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.IntializeDbAsync();

app.RegisterGameStoreEndpoints();

app.UseHttpLogging();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.Run();

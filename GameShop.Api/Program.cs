using GameShop.Api.Authorization;
using GameShop.Api.Data;
using GameShop.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameShopAuthorization();
builder.Services.AddHttpLogging(options => { });

var app = builder.Build();

await app.Services.IntializeDbAsync();

app.RegisterGameStoreEndpoints();
app.UseHttpLogging();

app.Run();

using GameShop.Api.Data;
using GameShop.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

await app.Services.IntializeDbAsync();

app.RegisterGameStoreEndpoints();

app.Run();

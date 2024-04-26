using GameShop.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.RegisterGameStoreEndpoints();

app.Run();

using GameShop.Api.Endpoints;
using GameShop.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var GameShopDBConnectionString = builder.Configuration.GetConnectionString("GameShopDB");

var app = builder.Build();

app.RegisterGameStoreEndpoints();

app.Run();

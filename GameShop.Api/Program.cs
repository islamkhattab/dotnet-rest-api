using GameShop.Api.Data;
using GameShop.Api.Endpoints;
using GameShop.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var GameShopDBConnectionString = builder.Configuration.GetConnectionString("GameShopDB");
builder.Services.AddSqlServer<GameShopContext>($"{GameShopDBConnectionString}");

var app = builder.Build();

app.Services.IntializeDb();

app.RegisterGameStoreEndpoints();

app.Run();

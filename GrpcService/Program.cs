using Business;
using Data;
using GrpcService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<BusinessAssemblyType>();
});

// Adicionar o DbContext
builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseInMemoryDatabase("Database"));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AlbumService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

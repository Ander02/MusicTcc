using Business;
using Data;
using GraphQLApi.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();

// Adicionar o DbContext
builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseInMemoryDatabase("Database"));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<BusinessAssemblyType>();
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>(config =>
    {
    })
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGraphQL();

app.Run();

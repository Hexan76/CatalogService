using CatalogService.Api;
using CatalogService.Application;
using CatalogService.Infrastructure;

using Framework.BuildingBlock.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFrameworkJson()
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi(builder);

var app = builder.Build();

app.UseApi();

app.Run();

using Framework.BuildingBlock.HttpApi;

namespace CatalogService.Api;

public static class OpenApiDefinitions
{
    public static OpenApiDefinitionOptions[] GetOpenApiDefinitions()
    {
        return
        [
            new OpenApiDefinitionOptions
            {
                DocumentName = "Catalog Service Api",
                Title = "CatalogService Api",
                Version = "1.0.0",
                ApiVersion = 1,
                IsDefault = true,
                ExcludeNonFastEndpoints = true,
                EnableJWTBearerAuth=true,
                SecurityDefinitions=new Dictionary<string, NSwag.OpenApiSecurityScheme>
                {
                    {"Bearer",
                    new NSwag.OpenApiSecurityScheme
                        {
                            Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                        }
                    }
                },
                Servers =
                [
                   new() { Url = "https://core.qasedexpress.com/catalog", Description = "Production" },
                   new() { Url = "http://core.qasedexpress.com/catalog", Description = "HTTP" },
                   new() { Url = "http://localhost:5146", Description = "Local" }
                ]
            }
        ];
    }
}

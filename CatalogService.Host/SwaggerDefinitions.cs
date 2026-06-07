using Framework.BuildingBlock.HttpApi;

namespace CatalogService.Host;

public class SwaggerDefinitions
{
    public static SwaggerModuleOptions[] GetSwaggerModules()
    {
        return
        [
            new SwaggerModuleOptions
            {
                DocumentName = "CatalogService Api",
                Title = "CatalogService Api",
                Version = "Version 1.0.0",
                ApiVersion=1,
                EndpointFilter = ep =>
                    ep.Routes.Any(c => c.Contains("/CatalogService", StringComparison.OrdinalIgnoreCase)),
                ExcludeNonFastEndpoints = true,
                Headers = new List<SwaggerHeaderOption>
                {
                    //new SwaggerHeaderOption
                    //{
                    //    Name = "__Tenant",
                    //    Description = "Multi-tenant identifier",
                    //    Required = true
                    //},
                    new SwaggerHeaderOption
                    {
                        Name = "Accept-Language",
                        Description = "Culture Identifier",
                        Required = false
                    }
                }
            },

        ];
    }

}

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;
using Framework.Observability;
using Serilog;

namespace CatalogService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddBuildingBlockExceptionHandling();
        services.AddSingleton<IValidationFailureHandler, ValidationFailureHandler>();
        services.AddHealthChecks();
        services.AddControllers();
        services.AddFastEndpoints(c => c.IncludeAbstractValidators = true)
            .FrameworkNSwagDocsPerModule(OpenApiDefinitions.GetOpenApiDefinitions());

        services.AddObservability(builder.Configuration, builder.Host);

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseMiddleware<FrameworkExceptionMiddlware>();
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseFastEndpoints(cfg =>
        {
            cfg.Versioning.Prefix = "v";
            cfg.Endpoints.RoutePrefix = "api";
            cfg.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
            {
                IValidationFailureHandler validationFailureHandler =
                    ctx.RequestServices.GetRequiredService<IValidationFailureHandler>();
                return validationFailureHandler.BuildValidationResponseAsync(failures, ctx, statusCode);
            };
        });

        app.UseRedirectFromHome("/scalar");

        app.UseConfiguredScalar(OpenApiDefinitions.GetOpenApiDefinitions());

        app.MapHealthChecks("/health");

        return app;
    }
}

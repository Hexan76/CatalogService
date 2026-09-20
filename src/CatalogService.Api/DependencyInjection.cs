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

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.SetIsOriginAllowed(IsLocalDevOrigin)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseMiddleware<FrameworkExceptionMiddlware>();
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseCors();
        app.UseFastEndpoints(cfg =>
        {
            cfg.Versioning.Prefix = "v";
            cfg.Endpoints.RoutePrefix = "api";
            cfg.Versioning.PrependToRoute = true;
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

    private static bool IsLocalDevOrigin(string origin)
    {
        if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
        {
            return false;
        }

        var isLocalHost = uri.Host is "localhost" or "127.0.0.1";
        var isAllowedPort = uri.Port is >= 3000 and <= 3100;

        return isLocalHost && isAllowedPort;
    }
}

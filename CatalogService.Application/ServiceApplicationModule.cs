using CatalogService.Application.Contracts;
using CatalogService.Domain;
using CatalogService.Items;

using Framework.BuildingBlock.Application;
using Framework.RabbitMQ;

using MassTransit;

using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace CatalogService.Application;

[DependsOn(
    typeof(AbpLuckyPennyAutoMapperModule),
    typeof(BuildingBlockApplicationModule),
    typeof(CatalogServiceDomainModule),
    typeof(CatalogServiceApplicationContractsModule)
    )]
public class CatalogServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CatalogServiceApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CatalogServiceApplicationModule>(validate: true);
        });

        context.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(CatalogServiceApplicationModule).Assembly);
        });

        context.Services.AddRabbitMQConsumers([typeof(CatalogServiceAppService).Assembly]);

        //var rabbit = context.Configuration
        //   .GetSection("RabbitMQ");
        //context.Services.AddMassTransit(x =>
        //{
        //    x.UsingRabbitMq((context, cfg) =>
        //    {
        //        cfg.Host(rabbit["HostName"], ushort.Parse(rabbit["Port"]!), rabbit["VirtualHost"],
        //            h =>
        //            {
        //                h.Username(rabbit["UserName"]!);
        //                h.Password(rabbit["Password"]!);
        //            });

        //        cfg.Message<ItemCreatedEvent>(m =>
        //        {
        //            m.SetEntityName("catalog.events");
        //        });

        //        cfg.Publish<ItemCreatedEvent>(p =>
        //        {
        //            p.ExchangeType = ExchangeType.Topic;
        //        });

        //        cfg.ConfigureEndpoints(context);
        //    });
        //});
    }
}

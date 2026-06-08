FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY catalog-service/ ./catalog-service/
COPY building-block/ ./building-block/

RUN ls -la
RUN ls -la catalog-service
RUN ls -la building-block

WORKDIR /src/catalog-service

RUN ls -la
RUN ls -la CatalogService.Host

RUN dotnet restore CatalogService.Host/CatalogService.Host.csproj

RUN dotnet publish CatalogService.Host/CatalogService.Host.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:44381
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 44381

ENTRYPOINT ["dotnet", "CatalogService.Host.dll"]
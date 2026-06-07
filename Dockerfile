# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY catalog-service/ ./catalog-service/
COPY building-block/ ./building-block/

WORKDIR /src/catalog-service

RUN dotnet restore CatalogService.slnx

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
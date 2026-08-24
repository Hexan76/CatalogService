# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

RUN apt-get update \
    && apt-get install -y --no-install-recommends git \
    && rm -rf /var/lib/apt/lists/*

COPY catalog-service/ .

COPY building-block ../building-block

RUN dotnet restore src/CatalogService.Api/CatalogService.Api.csproj

RUN dotnet publish src/CatalogService.Api/CatalogService.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:44381
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 44381

ENTRYPOINT ["dotnet", "CatalogService.Api.dll"]
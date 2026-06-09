# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN apt-get update && apt-get install -y git

RUN git clone -b dev https://gitlab+deploy-token-2:gldt-cForGZgfnNQ-Gi6L1x4_@gitlab.bsla.dev/microservice/dotnet/building-block.git ../building-block

RUN dotnet restore CatalogService.Host/CatalogService.Host.csproj

RUN dotnet publish CatalogService.Host/CatalogService.Host.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:44381
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 44381

ENTRYPOINT ["dotnet", "CatalogService.Host.dll"]
# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

RUN apt-get update \
    && apt-get install -y --no-install-recommends git \
    && rm -rf /var/lib/apt/lists/*

COPY catalog-service/ .

COPY building-block ../building-block

RUN dotnet restore src/CatalogService.Api/CatalogService.Api.csproj

ARG VERSION=1.0.0

RUN APP_VERSION="$(printf '%s' "$VERSION" | sed -E 's/^(dev|prod)-//')" \
    && echo "Stamping assemblies with $APP_VERSION" \
    && dotnet publish src/CatalogService.Api/CatalogService.Api.csproj \
        -c Release \
        --no-restore \
        -o /app/publish \
        /p:UseAppHost=false \
        /p:Version=$APP_VERSION \
        /p:AssemblyVersion=$APP_VERSION \
        /p:FileVersion=$APP_VERSION \
        /p:InformationalVersion=$APP_VERSION

FROM --platform=$TARGETPLATFORM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

ARG EXPOSE_PORT
ARG VERSION=1.0.0

WORKDIR /app

COPY --from=build /app/publish .

ENV APP_VERSION=${VERSION}
ENV ASPNETCORE_URLS=http://+:${EXPOSE_PORT}
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE ${EXPOSE_PORT}

ENTRYPOINT ["dotnet", "CatalogService.Api.dll"]
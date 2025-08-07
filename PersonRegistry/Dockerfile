# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PersonRegistry/PersonRegistry.Api.csproj", "PersonRegistry/"]
COPY ["PersonRegistry.Application/PersonRegistry.Application.csproj", "PersonRegistry.Application/"]
COPY ["PersonRegistry.Domain/PersonRegistry.Domain.csproj", "PersonRegistry.Domain/"]
COPY ["PersonRegistry.Common/PersonRegistry.Common.csproj", "PersonRegistry.Common/"]
COPY ["PersonRegistry.Infrastructure/PersonRegistry.Infrastructure.csproj", "PersonRegistry.Infrastructure/"]
RUN dotnet restore "./PersonRegistry/PersonRegistry.Api.csproj"
COPY . .
WORKDIR "/src/PersonRegistry"
RUN dotnet build "./PersonRegistry.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish to a final folder
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PersonRegistry.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonRegistry.Api.dll"]
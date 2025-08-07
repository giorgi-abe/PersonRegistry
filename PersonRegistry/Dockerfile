# Use the official ASP.NET image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["PersonRegistry.Api/PersonRegistry.Api.csproj", "PersonRegistry.Api/"]
COPY ["PersonRegistry.Application/PersonRegistry.Application.csproj", "PersonRegistry.Application/"]
COPY ["PersonRegistry.Infrastructure/PersonRegistry.Infrastructure.csproj", "PersonRegistry.Infrastructure/"]
COPY ["PersonRegistry.Domain/PersonRegistry.Domain.csproj", "PersonRegistry.Domain/"]
COPY ["PersonRegistry.Common/PersonRegistry.Common.csproj", "PersonRegistry.Common/"]

RUN dotnet restore "PersonRegistry.Api/PersonRegistry.Api.csproj"
COPY . .

WORKDIR "/src/PersonRegistry.Api"
RUN dotnet build "PersonRegistry.Api.csproj" -c Release -o /app/build

# Publish to a final folder
FROM build AS publish
RUN dotnet publish "PersonRegistry.Api.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonRegistry.Api.dll"]
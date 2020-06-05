FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/buildingblocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "src/buildingblocks/BuildingBlocks.Domain/"]
COPY ["src/buildingblocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/buildingblocks/BuildingBlocks.Infrastructure/"]
COPY ["src/microservices/Activity/Activity.API/Activity.API.csproj", "src/microservices/Activity/Activity.API/"]
COPY ["src/microservices/Activity/Activity.Application/Activity.Application.csproj", "src/microservices/Activity/Activity.Application/"]
COPY ["src/microservices/Activity/Activity.Domain/Activity.Domain.csproj", "src/microservices/Activity/Activity.Domain/"]
COPY ["src/microservices/Activity/Activity.Infrastructure/Activity.Infrastructure.csproj", "src/microservices/Activity/Activity.Infrastructure/"]

RUN dotnet restore "src/microservices/Activity/Activity.API/Activity.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/Activity/Activity.API"
RUN dotnet build "Activity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Activity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Together.Activity.API.dll"]
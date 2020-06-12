FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/microservices/Messaging/Messaging.API/Messaging.API.csproj", "src/microservices/Messaging/Messaging.API/"]
COPY ["src/microservices/Messaging/Messaging.Application/Messaging.Application.csproj", "src/microservices/Messaging/Messaging.Application/"]
COPY ["src/microservices/Messaging/Messaging.Infrastructure/Messaging.Infrastructure.csproj", "src/microservices/Messaging/Messaging.Infrastructure/"]
COPY ["src/microservices/Messaging/Messaging.Domain/Messaging.Domain.csproj", "src/microservices/Messaging/Messaging.Domain/"]
COPY ["src/buildingblocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "src/buildingblocks/BuildingBlocks.Domain/"]
COPY ["src/buildingblocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/buildingblocks/BuildingBlocks.Infrastructure/"]
RUN dotnet restore "src/microservices/Messaging/Messaging.API/Messaging.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/Messaging/Messaging.API"
RUN dotnet build "Messaging.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messaging.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Together.Messaging.API.dll"]
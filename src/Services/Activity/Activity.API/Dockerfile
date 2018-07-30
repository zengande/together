FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Activity/Activity.API/Activity.API.csproj src/Services/Activity/Activity.API/
RUN dotnet restore src/Services/Activity/Activity.API/Activity.API.csproj
COPY . .
WORKDIR /src/src/Services/Activity/Activity.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Activity.API.dll"]

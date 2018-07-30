FROM microsoft/dotnet:2.1.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/WebApps/WebMVC/WebMVC.csproj src/WebApps/WebMVC/
RUN dotnet restore src/WebApps/WebMVC/WebMVC.csproj
COPY . .
WORKDIR /src/src/WebApps/WebMVC
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMVC.dll"]

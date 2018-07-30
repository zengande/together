FROM microsoft/dotnet:2.1.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Location/Location.API/Location.API.csproj src/Services/Location/Location.API/
RUN dotnet restore src/Services/Location/Location.API/Location.API.csproj
COPY . .
WORKDIR /src/src/Services/Location/Location.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Location.API.dll"]

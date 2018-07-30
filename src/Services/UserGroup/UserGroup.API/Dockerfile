FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/UserGroup/UserGroup.API/UserGroup.API.csproj src/Services/UserGroup/UserGroup.API/
RUN dotnet restore src/Services/UserGroup/UserGroup.API/UserGroup.API.csproj
COPY . .
WORKDIR /src/src/Services/UserGroup/UserGroup.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserGroup.API.dll"]

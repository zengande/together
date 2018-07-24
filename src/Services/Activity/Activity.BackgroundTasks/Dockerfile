FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 49336
EXPOSE 44360

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Activity/Activity.BackgroundTasks/Activity.BackgroundTasks.csproj src/Services/Activity/Activity.BackgroundTasks/
RUN dotnet restore src/Services/Activity/Activity.BackgroundTasks/Activity.BackgroundTasks.csproj
COPY . .
WORKDIR /src/src/Services/Activity/Activity.BackgroundTasks
RUN dotnet build Activity.BackgroundTasks.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Activity.BackgroundTasks.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Activity.BackgroundTasks.dll"]

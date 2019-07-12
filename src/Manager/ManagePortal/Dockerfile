FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Manager/ManagePortal/ManagePortal.csproj", "src/Manager/ManagePortal/"]
RUN dotnet restore "src/Manager/ManagePortal/ManagePortal.csproj"
COPY . .
WORKDIR "/src/src/Manager/ManagePortal"
RUN dotnet build "ManagePortal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ManagePortal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ManagePortal.dll"]
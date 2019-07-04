FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Identity/Identity.API/Identity.API.csproj src/Services/Identity/Identity.API/
RUN dotnet restore src/Services/Identity/Identity.API/Identity.API.csproj
COPY . .
WORKDIR /src/src/Services/Identity/Identity.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.API.dll"]

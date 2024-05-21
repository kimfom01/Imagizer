FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY server/Imagizer.Api/Imagizer.Api.csproj Imagizer.Api/
RUN dotnet restore Imagizer.Api/Imagizer.Api.csproj
COPY server/Imagizer.Api/ Imagizer.Api/
RUN dotnet build Imagizer.Api/Imagizer.Api.csproj --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Imagizer.Api/Imagizer.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagizer.Api.dll"]

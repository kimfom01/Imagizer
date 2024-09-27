FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.20 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Imagizer.Api.csproj", "Imagizer.Api/"]
RUN dotnet restore "Imagizer.Api/Imagizer.Api.csproj"
COPY . Imagizer.Api/
WORKDIR "/src/Imagizer.Api"
RUN dotnet build "Imagizer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Imagizer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Imagizer.Api.dll"]

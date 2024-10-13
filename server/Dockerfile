FROM mcr.microsoft.com/dotnet/aspnet:8.0.10-alpine3.20 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.403 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ImagizerAPI/ImagizerAPI.csproj", "ImagizerAPI/"]
COPY ["ServiceDefaults/ServiceDefaults.csproj", "ServiceDefaults/"]
RUN dotnet restore "ImagizerAPI/ImagizerAPI.csproj"
COPY . .
WORKDIR "/src/ImagizerAPI"
RUN dotnet build "ImagizerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ImagizerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImagizerAPI.dll"]

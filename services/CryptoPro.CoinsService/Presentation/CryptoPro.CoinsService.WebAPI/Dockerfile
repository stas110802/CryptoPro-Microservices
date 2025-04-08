FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/CryptoPro.CoinsService/Presentation/CryptoPro.CoinsService.WebAPI/CryptoPro.CoinsService.WebAPI.csproj", "services/CryptoPro.CoinsService/Presentation/CryptoPro.CoinsService.WebAPI/"]
RUN dotnet restore "services/CryptoPro.CoinsService/Presentation/CryptoPro.CoinsService.WebAPI/CryptoPro.CoinsService.WebAPI.csproj"
COPY . .
WORKDIR "/src/services/CryptoPro.CoinsService/Presentation/CryptoPro.CoinsService.WebAPI"
RUN dotnet build "CryptoPro.CoinsService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CryptoPro.CoinsService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoPro.CoinsService.WebAPI.dll"]

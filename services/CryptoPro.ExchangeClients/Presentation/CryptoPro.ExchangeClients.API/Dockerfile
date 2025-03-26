FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/CryptoPro.ExchangeClients/Presentation/CryptoPro.ExchangeClients.API/CryptoPro.ExchangeClients.API.csproj", "services/CryptoPro.ExchangeClients/Presentation/CryptoPro.ExchangeClients.API/"]
RUN dotnet restore "services/CryptoPro.ExchangeClients/Presentation/CryptoPro.ExchangeClients.API/CryptoPro.ExchangeClients.API.csproj"
COPY . .
WORKDIR "/src/services/CryptoPro.ExchangeClients/Presentation/CryptoPro.ExchangeClients.API"
RUN dotnet build "CryptoPro.ExchangeClients.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CryptoPro.ExchangeClients.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoPro.ExchangeClients.API.dll"]

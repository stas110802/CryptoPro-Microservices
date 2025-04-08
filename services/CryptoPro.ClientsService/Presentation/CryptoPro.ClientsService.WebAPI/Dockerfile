FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/CryptoPro.ClientsService/Presentation/CryptoPro.ClientsService.WebAPI/CryptoPro.ClientsService.WebAPI.csproj", "services/CryptoPro.ClientsService/Presentation/CryptoPro.ClientsService.WebAPI/"]
RUN dotnet restore "services/CryptoPro.ClientsService/Presentation/CryptoPro.ClientsService.WebAPI/CryptoPro.ClientsService.WebAPI.csproj"
COPY . .
WORKDIR "/src/services/CryptoPro.ClientsService/Presentation/CryptoPro.ClientsService.WebAPI"
RUN dotnet build "CryptoPro.ClientsService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CryptoPro.ClientsService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoPro.ClientsService.WebAPI.dll"]

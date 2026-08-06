FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AirbnbClone.Api/AirbnbClone.Api.csproj", "AirbnbClone.Api/"]
COPY ["AirbnbClone.Application/AirbnbClone.Application.csproj", "AirbnbClone.Application/"]
COPY ["AirbnbClone.Domain/AirbnbClone.Domain.csproj", "AirbnbClone.Domain/"]
COPY ["AirbnbClone.Infrastructure/AirbnbClone.Infrastructure.csproj", "AirbnbClone.Infrastructure/"]
RUN dotnet restore "AirbnbClone.Api/AirbnbClone.Api.csproj"
COPY . .
WORKDIR "/src/AirbnbClone.Api"
RUN dotnet build "./AirbnbClone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AirbnbClone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirbnbClone.Api.dll"]

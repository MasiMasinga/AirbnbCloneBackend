FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY AirbnbClone.sln ./
COPY AirbnbClone.Domain/AirbnbClone.Domain.csproj AirbnbClone.Domain/
COPY AirbnbClone.Application/AirbnbClone.Application.csproj AirbnbClone.Application/
COPY AirbnbClone.Infrastructure/AirbnbClone.Infrastructure.csproj AirbnbClone.Infrastructure/
COPY AirbnbClone.Api/AirbnbClone.Api.csproj AirbnbClone.Api/

RUN dotnet restore AirbnbClone.Api/AirbnbClone.Api.csproj

COPY AirbnbClone.Domain/ AirbnbClone.Domain/
COPY AirbnbClone.Application/ AirbnbClone.Application/
COPY AirbnbClone.Infrastructure/ AirbnbClone.Infrastructure/
COPY AirbnbClone.Api/ AirbnbClone.Api/

RUN dotnet publish AirbnbClone.Api/AirbnbClone.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AirbnbClone.Api.dll"]
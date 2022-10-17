FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["BuildingBlocks/BuildingBlocks.Authentication/BuildingBlocks.Authentication.csproj", "BuildingBlocks/BuildingBlocks.Authentication/"]
COPY ["BuildingBlocks/BuildingBlocks.BlobStorage/BuildingBlocks.BlobStorage.csproj", "BuildingBlocks/BuildingBlocks.BlobStorage/"]
COPY ["BuildingBlocks/BuildingBlocks.Cache/BuildingBlocks.Cache.csproj", "BuildingBlocks/BuildingBlocks.Cache/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Extensions/BuildingBlocks.Extensions.csproj", "BuildingBlocks/BuildingBlocks.Extensions/"]
COPY ["BuildingBlocks/BuildingBlocks.Mongo/BuildingBlocks.Mongo.csproj", "BuildingBlocks/BuildingBlocks.Mongo/"]


COPY ["Services/IdentityService/IdentityService.Contracts/IdentityService.Contracts.csproj", "Services/IdentityService/IdentityService.Contracts/"]
COPY ["Services/IdentityService/IdentityService.Core/IdentityService.Core.csproj", "Services/IdentityService/IdentityService.Core/"]
COPY ["Services/IdentityService/IdentityService.Infrastructure/IdentityService.Infrastructure.csproj", "Services/IdentityService/IdentityService.Infrastructure/"]
COPY ["Services/IdentityService/IdentityService.Application/IdentityService.Application.csproj", "Services/IdentityService/IdentityService.Application/"]
COPY ["Services/IdentityService/IdentityService.API/IdentityService.API.csproj", "Services/IdentityService/IdentityService.API/"]

RUN dotnet restore "Services/IdentityService/IdentityService.API/IdentityService.API.csproj"
COPY . .
WORKDIR "/src/Services/IdentityService/IdentityService.API"
RUN dotnet build "IdentityService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.API.dll"]
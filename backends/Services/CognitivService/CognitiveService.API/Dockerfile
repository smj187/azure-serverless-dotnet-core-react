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


COPY ["Services/CognitiveService/CognitiveService.Contracts/CognitiveService.Contracts.csproj", "Services/CognitiveService/CognitiveService.Contracts/"]
COPY ["Services/CognitiveService/CognitiveService.Core/CognitiveService.Core.csproj", "Services/CognitiveService/CognitiveService.Core/"]
COPY ["Services/CognitiveService/CognitiveService.Infrastructure/CognitiveService.Infrastructure.csproj", "Services/CognitiveService/CognitiveService.Infrastructure/"]
COPY ["Services/CognitiveService/CognitiveService.Application/CognitiveService.Application.csproj", "Services/CognitiveService/CognitiveService.Application/"]
COPY ["Services/CognitiveService/CognitiveService.API/CognitiveService.API.csproj", "Services/CognitiveService/CognitiveService.API/"]

RUN dotnet restore "Services/CognitiveService/CognitiveService.API/CognitiveService.API.csproj"
COPY . .
WORKDIR "/src/Services/CognitiveService/CognitiveService.API"
RUN dotnet build "CognitiveService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CognitiveService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CognitiveService.API.dll"]
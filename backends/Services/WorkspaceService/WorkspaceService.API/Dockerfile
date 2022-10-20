FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ["Services/WorkspaceService/WorkspaceService.API/Certificates/", "/https/"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["BuildingBlocks/BuildingBlocks.Authentication/BuildingBlocks.Authentication.csproj", "BuildingBlocks/BuildingBlocks.Authentication/"]
COPY ["BuildingBlocks/BuildingBlocks.BlobStorage/BuildingBlocks.BlobStorage.csproj", "BuildingBlocks/BuildingBlocks.BlobStorage/"]
COPY ["BuildingBlocks/BuildingBlocks.Cache/BuildingBlocks.Cache.csproj", "BuildingBlocks/BuildingBlocks.Cache/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Extensions/BuildingBlocks.Extensions.csproj", "BuildingBlocks/BuildingBlocks.Extensions/"]
COPY ["BuildingBlocks/BuildingBlocks.Mongo/BuildingBlocks.Mongo.csproj", "BuildingBlocks/BuildingBlocks.Mongo/"]


COPY ["Services/WorkspaceService/WorkspaceService.Contracts/WorkspaceService.Contracts.csproj", "Services/WorkspaceService/WorkspaceService.Contracts/"]
COPY ["Services/WorkspaceService/WorkspaceService.Core/WorkspaceService.Core.csproj", "Services/WorkspaceService/WorkspaceService.Core/"]
COPY ["Services/WorkspaceService/WorkspaceService.Infrastructure/WorkspaceService.Infrastructure.csproj", "Services/WorkspaceService/WorkspaceService.Infrastructure/"]
COPY ["Services/WorkspaceService/WorkspaceService.Application/WorkspaceService.Application.csproj", "Services/WorkspaceService/WorkspaceService.Application/"]
COPY ["Services/WorkspaceService/WorkspaceService.API/WorkspaceService.API.csproj", "Services/WorkspaceService/WorkspaceService.API/"]

RUN dotnet restore "Services/WorkspaceService/WorkspaceService.API/WorkspaceService.API.csproj"
COPY . .
WORKDIR "/src/Services/WorkspaceService/WorkspaceService.API"
RUN dotnet build "WorkspaceService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkspaceService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkspaceService.API.dll"]
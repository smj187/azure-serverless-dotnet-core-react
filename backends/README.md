Identity Service HTTPS Setup

```
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\IdentityService.pfx -p crypticpassword
dotnet user-secrets -p .\Services\IdentityService\IdentityService.API\IdentityService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose.yaml up --build
```

Cognitive Service HTTPS Setup

```
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\CognitiveService.pfx -p crypticpassword
dotnet user-secrets -p .\Services\CognitivService\CognitiveService.API\CognitiveService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose-cognitive.yaml up --build
```

```
// config service https
openssl pkcs12 -export -out Services/CognitiveService/CognitiveService.API/Certificates/CognitiveService.pfx -inkey .\keystore\server.key -in .\keystore\server.crt

```

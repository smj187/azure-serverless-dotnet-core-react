Identity Service HTTP Setup

```
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\IdentityService.pfx -p crypticpassword
dotnet user-secrets -p .\Services\IdentityService\IdentityService.API\IdentityService.API.csproj set "Kestrel:Certificates:Production:Password" "crypticpassword"
dotnet dev-certs https --trust

docker-compose -f docker-compose.yaml up --build
```

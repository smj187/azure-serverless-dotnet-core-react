### HTTPS certificates generation

1. generate root certificate and install it (as admin)

```sh
./create-ca-cert.sh
```

2. install `ca.crt` from `/certificates`

3. generate https certificates for gateway and apps

```sh
./create-cert.sh
```

4. generate https certificates for services

```sh
// cognitive service
openssl pkcs12 -export -out backends/Services/CognitiveService/CognitiveService.API/Certificates/CognitiveService.pfx -inkey .\certificates\server.key -in .\certificates\server.crt

// identity service
openssl pkcs12 -export -out backends/Services/IdentityService/IdentityService.API/Certificates/IdentityService.pfx -inkey .\certificates\server.key -in .\certificates\server.crt
```

```
// config service https
openssl pkcs12 -export -out Services/CognitiveService/CognitiveService.API/Certificates/CognitiveService.pfx -inkey .\keystore\server.key -in .\keystore\server.crt

```

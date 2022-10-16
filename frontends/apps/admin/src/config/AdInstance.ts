import {
  Configuration,
  LogLevel,
  PublicClientApplication
} from "@azure/msal-browser"

const msalConfig: Configuration = {
  auth: {
    clientId: "fce71c0c-5f69-4b6f-ac2c-79fd015cfe4e",
    authority:
      "https://login.microsoftonline.com/eec93096-2e1d-4a28-972f-95df728d60d0",
    redirectUri: "http://localhost:3001",
    postLogoutRedirectUri: "http://localhost:3001"
  },
  cache: {
    cacheLocation: "localStorage"
  },
  system: {
    loggerOptions: {
      loggerCallback: (
        level: LogLevel,
        message: string,
        containsPii: boolean
      ) => {
        if (containsPii) {
          return
        }
        switch (level) {
          case LogLevel.Error:
            console.error(message)
            return
          case LogLevel.Info:
            console.info(message)
            return
          case LogLevel.Verbose:
            console.debug(message)
            return
          case LogLevel.Warning:
            console.warn(message)
            return
          default:
            return
        }
      },
      logLevel: LogLevel.Verbose
    }
  }
}

export const AdInstance = new PublicClientApplication(msalConfig)

export const scopes = [
  "https://carnivalai.onmicrosoft.com/permissions/admin-privileges"
]

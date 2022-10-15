import {
  Configuration,
  LogLevel,
  PublicClientApplication
} from "@azure/msal-browser"

const b2cPolicies = {
  names: {
    signUpSignIn: "B2C_1_sign_up",
    forgotPassword: "b2c_1_reset",
    editProfile: "b2c_1_edit_profile"
  },
  authorities: {
    signUpSignIn: {
      authority:
        "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_sign_up"
    },
    signIn: {
      authority:
        "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_sign_in"
    },
    forgotPassword: {
      authority:
        "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_reset_password"
    },
    editProfile: {
      authority:
        "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_edit_profile"
    }
  },
  authorityDomain: "carnivalai.b2clogin.com"
}

const msalConfig: Configuration = {
  auth: {
    clientId: "df2a7179-a94f-4018-84e2-812ebdf7f148",
    authority: b2cPolicies.authorities.signIn.authority,
    knownAuthorities: [b2cPolicies.authorityDomain],
    redirectUri: "http://localhost:3000",
    postLogoutRedirectUri: "http://localhost:3000"
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

export const B2cInstance = new PublicClientApplication(msalConfig)

export const scopes = [
  "https://carnivalai.onmicrosoft.com/permissions/customer-privileges",
  "https://carnivalai.onmicrosoft.com/permissions/cognitive-services",
  "https://carnivalai.onmicrosoft.com/permissions/identity-services",
  "https://carnivalai.onmicrosoft.com/permissions/workspace-services"
]

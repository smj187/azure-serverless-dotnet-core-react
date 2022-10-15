import { useAuthStore } from "@/store"
import {
  AccountInfo,
  AuthenticationResult,
  EventMessage,
  EventMessageUtils,
  EventType,
  InteractionStatus,
  PublicClientApplication
} from "@azure/msal-browser"
import { Router } from "vue-router"
import { msalConfig } from "./config"

type AccountIdentifiers = Partial<
  Pick<AccountInfo, "homeAccountId" | "localAccountId" | "username">
>

/**
 * Helper function to determine whether 2 arrays are equal
 * Used to avoid unnecessary state updates
 * @param arrayA
 * @param arrayB
 */
function accountArraysAreEqual(
  arrayA: Array<AccountIdentifiers>,
  arrayB: Array<AccountIdentifiers>
): boolean {
  if (arrayA.length !== arrayB.length) {
    return false
  }

  const comparisonArray = [...arrayB]

  return arrayA.every(elementA => {
    const elementB = comparisonArray.shift()
    if (!elementA || !elementB) {
      return false
    }

    return (
      elementA.homeAccountId === elementB.homeAccountId &&
      elementA.localAccountId === elementB.localAccountId &&
      elementA.username === elementB.username
    )
  })
}

const scopes = [
  "https://carnivalai.onmicrosoft.com/permissions/customer-privileges",
  "https://carnivalai.onmicrosoft.com/permissions/cognitive-services",
  "https://carnivalai.onmicrosoft.com/permissions/identity-services",
  "https://carnivalai.onmicrosoft.com/permissions/workspace-services"
]

class MsalInstance {
  private static _instance: PublicClientApplication
  private static _accounts: AccountInfo[] = []
  private static _inProgress: InteractionStatus = InteractionStatus.Startup

  private constructor() {}

  public static getMsalInstance(): PublicClientApplication {
    if (this._instance) return this._instance

    this._instance = new PublicClientApplication(msalConfig)
    const { initializeUserAccount } = useAuthStore()
    console.log("i have a user", this._instance.getAllAccounts())

    if (this._instance.getAllAccounts().length > 0) {
      initializeUserAccount(this._instance.getAllAccounts()[0])
    }

    this._instance.addEventCallback((message: EventMessage) => {
      switch (message.eventType) {
        case EventType.LOGIN_START:
          console.log("LOGIN_START", message)
          break

        case EventType.LOGIN_FAILURE:
          console.log("LOGIN_FAILURE", message)
          break

        case EventType.LOGIN_SUCCESS:
          console.log("LOGIN_SUCCESS", message)
          if (message.payload) {
            const payload = message.payload as AuthenticationResult
            const b2cAccount = payload.account

            initializeUserAccount(b2cAccount)
            // assign new active account
            this._instance.setActiveAccount(b2cAccount)

            console.log(
              ":: newUser ::",
              (message.payload as any).account?.idTokenClaims
                ?.newUser as boolean
            )

            console.log(":: token ::", (message.payload as any).accessToken)
            console.log(":: account ::", (message.payload as any)?.account)
          } else {
            console.error(message)
            throw new Error("what happend?")
          }

          break

        case EventType.LOGOUT_SUCCESS:
          console.log("LOGOUT_SUCCESS", message)

          break

        case EventType.LOGOUT_START:
          console.log("LOGOUT_START", message)
          break

        case EventType.ACCOUNT_ADDED:
          console.log("ACCOUNT_ADDED", message)
          break

        case EventType.ACCOUNT_REMOVED:
          console.log("ACCOUNT_REMOVED", message)
          break

        case EventType.LOGIN_SUCCESS:
          console.log("LOGIN_SUCCESS", message)
          break

        case EventType.SSO_SILENT_SUCCESS:
          console.log("SSO_SILENT_SUCCESS", message)
          break

        case EventType.HANDLE_REDIRECT_END:
          console.log("HANDLE_REDIRECT_END", message)
          break

        case EventType.LOGIN_FAILURE:
          console.log("LOGIN_FAILURE", message)
          break

        case EventType.SSO_SILENT_FAILURE:
          console.log("SSO_SILENT_FAILURE", message)
          break

        case EventType.LOGOUT_END:
          console.log("LOGOUT_END", message)
          break

        case EventType.ACQUIRE_TOKEN_SUCCESS:
          console.log("ACQUIRE_TOKEN_SUCCESS", message)
          break

        case EventType.ACQUIRE_TOKEN_FAILURE:
          console.log("ACQUIRE_TOKEN_FAILURE", message)

          const currentAccounts = this._instance.getAllAccounts()
          if (!accountArraysAreEqual(currentAccounts, this._accounts)) {
            this._accounts = currentAccounts
          }
          break
      }

      const status = EventMessageUtils.getInteractionStatusFromEvent(
        message,
        this._inProgress
      )
      if (status !== null) {
        this._inProgress = status
      }
    })

    if (this._inProgress === InteractionStatus.Startup) {
      this._instance.handleRedirectPromise().catch(err => {
        console.log("an error occured", err)

        // Errors should be handled by listening to the LOGIN_FAILURE event
        return
      })
    }

    return this._instance
  }

  static async getToken() {
    const { accessToken } = await this._instance.acquireTokenSilent({
      scopes: [...scopes]
    })

    return accessToken
  }

  static signInRequest() {
    this._instance.loginRedirect({
      scopes: [...scopes],
      authority:
        "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_sign_in",
      redirectStartPage: "/about"
    })
  }

  static signUpRequest() {
    this._instance.loginRedirect({
      scopes: [...scopes],
      authority:
        "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_sign_up"
      // redirectStartPage: "/welcome"
    })
  }

  static signOutRequest() {
    this._instance.logoutRedirect({
      postLogoutRedirectUri: "/"
    })
  }
}

export { MsalInstance }

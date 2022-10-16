import { App, computed, onMounted, reactive, ref } from "vue"
import {
  RouteLocationNamedRaw,
  RouteLocationNormalized,
  Router,
  RouteRecordNormalized
} from "vue-router"
import { InjectionKey } from "vue"
import {
  AccountInfo,
  AuthenticationResult,
  EventMessage,
  EventMessageUtils,
  EventType,
  InteractionStatus,
  PublicClientApplication
} from "@azure/msal-browser"
import { MsalInjectionKey } from "@/@symbols"
import { CustomNavigationClient } from "@/router/CustomNavigationClient"
import { scopes } from "@/config/AdInstance"

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

interface State {
  accounts: Array<AccountInfo>
  inProgress: InteractionStatus
}

export const msalPlugin = {
  install: (app: App, router: Router, AdInstance: PublicClientApplication) => {
    const navigationClient = new CustomNavigationClient(router)
    AdInstance.setNavigationClient(navigationClient)

    const account = ref<AccountInfo | null>(null)
    const isAuthenticated = computed(() => account.value !== null)

    const state = reactive<State>({
      accounts: [],
      inProgress: InteractionStatus.Startup
    })

    // const inProgress = InteractionStatus.Startup
    const accounts = AdInstance.getAllAccounts()
    state.accounts = accounts
    if (accounts.length > 0) {
      AdInstance.setActiveAccount(accounts[0])
    }
    AdInstance.addEventCallback(event => {
      if (event.eventType === EventType.LOGIN_SUCCESS && event.payload) {
        const payload = event.payload as AuthenticationResult
        const account = payload.account
        AdInstance.setActiveAccount(account)
      }
    })

    account.value = AdInstance.getActiveAccount() ?? null

    AdInstance.addEventCallback((message: EventMessage) => {
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

            // assign new active account
            AdInstance.setActiveAccount(payload.account)
            account.value = AdInstance.getActiveAccount()

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

          const currentAccounts = AdInstance.getAllAccounts()
          if (!accountArraysAreEqual(currentAccounts, state.accounts)) {
            state.accounts = currentAccounts
          }
          break
      }

      const status = EventMessageUtils.getInteractionStatusFromEvent(
        message,
        state.inProgress
      )
      if (status !== null) {
        state.inProgress = status
      }
    })

    if (state.inProgress === InteractionStatus.Startup) {
      AdInstance.handleRedirectPromise().catch(err => {
        console.error("err", err)
        // Errors should be handled by listening to the LOGIN_FAILURE event
        return
      })
    }

    router.beforeEach(
      async (to: RouteLocationNormalized, from: RouteLocationNormalized) => {
        if (to.meta.requiresAuth) {
          return AdInstance.handleRedirectPromise()
            .then(x => {
              if (isAuthenticated.value === false) {
                return "Login"
              }
              return true
            })
            .catch(err => {
              console.error(err)
              return false
            })
        }

        return true
      }
    )

    function redirectToSignIn() {
      if (AdInstance === null) {
        throw new Error("initializeMsal() was not initialized")
      }

      AdInstance.loginRedirect({
        scopes: [...scopes]
        // authority:
        //   "https://carnivalai.b2clogin.com/carnivalai.onmicrosoft.com/B2C_1_sign_in",
        // redirectStartPage: "/"
      })
    }

    function redirectToSignOut() {
      if (AdInstance === null) {
        throw new Error("initializeMsal() was not initialized")
      }

      AdInstance.logoutRedirect({
        postLogoutRedirectUri: "/"
      })
    }

    app.provide(MsalInjectionKey, {
      account,
      isAuthenticated,

      redirectToSignIn,
      redirectToSignOut
    })
  }
}

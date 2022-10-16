import { AccountInfo, IPublicClientApplication } from "@azure/msal-browser"
import { ComputedRef, InjectionKey, Ref } from "vue"

export interface MsalPluginProvide {
  account: Ref<AccountInfo | null>
  isAuthenticated: ComputedRef<boolean>
  redirectToSignIn(): void
  redirectToSignOut(): void
}

export const MsalInjectionKey: InjectionKey<MsalPluginProvide> = Symbol("Msal")

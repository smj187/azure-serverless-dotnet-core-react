import { PublicClientApplication } from "@azure/msal-browser"
import "pinia"
import { Router } from "vue-router"

declare module "pinia" {
  export interface PiniaCustomProperties {
    msalInstance: PublicClientApplication
    router: Router
  }
}

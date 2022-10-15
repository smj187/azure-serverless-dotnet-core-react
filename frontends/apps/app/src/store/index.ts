import { AccountInfo } from "@azure/msal-browser"
import { defineStore } from "pinia"

type State = {
  b2cAccount: AccountInfo | null
}

export const useAuthStore = defineStore("auth-store", {
  state: (): State => ({
    b2cAccount: null
  }),

  getters: {
    isAuthenticated: (state: State) => state.b2cAccount !== null
  },

  actions: {
    initializeUserAccount(account: AccountInfo | null) {
      console.log("hello", account)
      this.b2cAccount = account
    }
  }
})

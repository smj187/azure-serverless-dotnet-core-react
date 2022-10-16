import { AccountTier, getAccountTiers } from "@/http/users/userService"
import { defineStore } from "pinia"

type State = {
  isLoaded: boolean
  accountTiers: Array<AccountTier>
}

export const useUserStore = defineStore("user-store", {
  state: (): State => ({
    isLoaded: false,
    accountTiers: []
  }),

  actions: {
    async initializeStore() {
      this.isLoaded = false
      const tiers = await getAccountTiers()

      this.accountTiers = tiers
      this.isLoaded = true
    }
  }
})

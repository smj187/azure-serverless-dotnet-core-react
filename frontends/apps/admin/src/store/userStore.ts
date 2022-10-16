import {
  AccountTier,
  findUser,
  getAccountTiers,
  getUsers,
  patchUserAccountTier,
  User
} from "@/http/users/userService"
import { defineStore } from "pinia"

type State = {
  isLoaded: boolean
  accountTiers: Array<AccountTier>
  users: Array<User>
  userDetails: User | null
}

export const useUserStore = defineStore("user-store", {
  state: (): State => ({
    isLoaded: false,
    accountTiers: [],
    users: [],
    userDetails: null
  }),

  actions: {
    async initializeStore() {
      this.isLoaded = false
      const tiers = await getAccountTiers()
      const users = await getUsers()

      this.accountTiers = tiers
      this.users = users
      this.isLoaded = true
    },

    async findUserAsync(userId: string) {
      this.userDetails = null
      this.userDetails = await findUser(userId)
    },

    async patchAccountTier(newAccountTierValue: number) {
      this.userDetails = await patchUserAccountTier(
        this.userDetails!.b2cObjectId,
        newAccountTierValue
      )
    }
  }
})

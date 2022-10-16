import { MsalInjectionKey } from "@/@symbols"
import { AccountInfo } from "@azure/msal-browser"
import { defineStore } from "pinia"
import { inject } from "vue"
import {
  getWeather,
  Weather,
  getWeatherAsAdmin
} from "@/http/weather/weatherService"
import { getPublic } from "@/http/public/publicService"

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
    async initializeUserAccount() {
      console.log("initializeUserAccount")
      // const w = await getWeather()
      // // const aw = await getWeatherAsAdmin()
      // // console.log(w, aw)
      // const data = await getPublic()
      // console.log(data)
    }
  }
})

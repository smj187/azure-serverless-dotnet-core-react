import { createApp, inject, markRaw } from "vue"
import App from "@/App.vue"
import router from "@/router"
import { createPinia } from "pinia"
import "@/assets/tailwind.css"
import { msalPlugin } from "./plugins/msalPlugin"
import { MsalInjectionKey } from "./@symbols"
import { B2cInstance } from "@/config/B2cInstance"

const app = createApp(App)

app.use(router)

app.use(createPinia())

// pinia.use(({ store }) => {
//
// })

app.use(msalPlugin, router, B2cInstance)

router.isReady().then(() => {
  app.mount("#app")
})

//

import { createApp, markRaw } from "vue"
import App from "@/App.vue"
import router from "@/router"
import { createPinia } from "pinia"
import "@/assets/tailwind.css"
import { CustomNavigationClient } from "@/router/CustomNavigationClient"
import { MsalInstance } from "@/azure/b2cClient"

const app = createApp(App)
app.use(router)

const pinia = createPinia()
app.use(pinia)

const navigationClient = new CustomNavigationClient(router)
MsalInstance.getMsalInstance().setNavigationClient(navigationClient)

pinia.use(({ store }) => {
  store.msalInstance = markRaw(MsalInstance.getMsalInstance())
  store.router = markRaw(router)
})

router.isReady().then(() => {
  app.mount("#app")
})


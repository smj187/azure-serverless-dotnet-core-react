import { createApp } from "vue"
import App from "@/App.vue"
import router from "@/router"
import { createPinia } from "pinia"
import "@/assets/tailwind.css"
import { AdInstance } from "@/config/AdInstance"
import { msalPlugin } from "./plugins/msalPlugin"

const app = createApp(App)
app.use(router)

app.use(createPinia())
app.use(msalPlugin, router, AdInstance)

router.isReady().then(() => {
  app.mount("#app")
})


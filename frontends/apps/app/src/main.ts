import { createApp } from "vue"
import App from "@/App.vue"
import router from "@/router"
import { createPinia } from "pinia"
import "@/assets/tailwind.css"

const app = createApp(App)
app.use(router)

app.use(createPinia())

router.isReady().then(() => {
  app.mount("#app")
})


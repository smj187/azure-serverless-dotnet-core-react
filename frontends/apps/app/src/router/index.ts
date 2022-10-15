import { MsalInstance } from "@/azure/b2cClient"
import {
  createRouter,
  createWebHistory,
  RouteLocationNormalized,
  RouteRecordRaw
} from "vue-router"

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: () => import("@/views/HomeView.vue")
  },
  {
    path: "/about",
    name: "About",
    component: () => import("@/views/AboutView.vue"),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/welcome",
    name: "Welcome",
    component: () => import("@/views/WelcomeView.vue"),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/unauthorized",
    name: "Unauthorized",
    component: () => import("@/views/UnauthorizedView.vue")
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes: [...routes]
})

router.beforeEach(
  async (to: RouteLocationNormalized, from: RouteLocationNormalized) => {
    // redirect to login if no auth is provided
    if (to.meta.requiresAuth) {
      console.log("to", to, "from", from)
      return MsalInstance.getMsalInstance()
        .handleRedirectPromise()
        .then(() => {
          const accounts = MsalInstance.getMsalInstance().getAllAccounts()

          // redirect to not authenticated view if there aren't any accounts in saml
          const isAuthenticated = accounts.length > 0 ? true : false

          if (isAuthenticated === false) {
            return "Unauthorized"
          }

          return true
        })
        .catch(() => {
          console.error("some auth error occured")
          return false
        })
    }

    return true
  }
)

export default router

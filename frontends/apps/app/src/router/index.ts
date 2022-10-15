import { MsalInjectionKey } from "@/@symbols"
import { IPublicClientApplication } from "@azure/msal-browser"
import { inject } from "vue"
import {
  createRouter,
  createWebHistory,
  RouteLocationNormalized,
  Router,
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

export default router

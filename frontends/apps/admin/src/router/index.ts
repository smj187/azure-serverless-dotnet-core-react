import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router"

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: () => import("@/views/HomeView.vue"),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("@/views/LoginView.vue")
  },

  {
    path: "/users",
    name: "Users",
    component: () => import("@/views/UsersView.vue"),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/users/:userId",
    name: "UserDetails",
    component: () => import("@/views/UserDetailsView.vue"),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/voices",
    name: "Voices",
    component: () => import("@/views/VoicesView.vue"),
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/profile",
    name: "Profile",
    component: () => import("@/views/ProfileView.vue"),
    meta: {
      requiresAuth: true
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes: [...routes]
})

export default router

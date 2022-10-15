<script setup lang="ts">
  import { RouterLink } from "vue-router"
  import { storeToRefs } from "pinia"
  import { useAuthStore } from "../store"
  import { inject } from "vue"
  import { MsalInjectionKey } from "@/@symbols"

  const {
    isAuthenticated,
    redirectToSignIn,
    redirectToSignOut,
    redirectToSignUp
  } = inject(MsalInjectionKey)!
</script>

<template>
  <header
    class="h-20 border-b border-slate-300/30 flex items-center justify-center space-x-3"
  >
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
    <span>|</span>
    <div class="flex items-center space-x-1" v-if="!isAuthenticated">
      <button @click="redirectToSignUp">sign up</button>
      <span>or</span>
      <button @click="redirectToSignIn">sign in</button>
    </div>
    <div v-else>
      <button @click="redirectToSignOut">sign out</button>
    </div>
  </header>
</template>

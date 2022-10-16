<script setup lang="ts">
  import { MsalInjectionKey } from "@/@symbols"
  import { inject } from "vue"
  import { RouterLink } from "vue-router"

  const { isAuthenticated, redirectToSignOut } = inject(MsalInjectionKey)!
</script>

<template>
  <header
    class="sticky top-0 left-0 w-full h-20 border-b border-slate-200/20 flex items-center justify-center space-x-3"
  >
    <span>Header</span>
    <span>|</span>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>

    <div v-if="isAuthenticated">
      <button @click="redirectToSignOut">sign out</button>
    </div>
    <div v-else>
      <RouterLink to="/login">Login</RouterLink>
    </div>
  </header>
</template>

<script setup lang="ts">
  import { RouterLink } from "vue-router"
  import { storeToRefs } from "pinia"
  import { useAuthStore } from "../store"
  import { MsalInstance } from "@/azure/b2cClient"
  const { isAuthenticated } = storeToRefs(useAuthStore())
</script>

<template>
  <header
    class="h-20 border-b border-slate-300/30 flex items-center justify-center space-x-3"
  >
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
    <span>|</span>
    <div class="flex items-center space-x-1" v-if="!isAuthenticated">
      <button @click="MsalInstance.signUpRequest">sign up</button>
      <span>or</span>
      <button @click="MsalInstance.signInRequest">sign in</button>
    </div>
    <div v-else>
      <button @click="MsalInstance.signOutRequest">sign out</button>
    </div>
  </header>
</template>

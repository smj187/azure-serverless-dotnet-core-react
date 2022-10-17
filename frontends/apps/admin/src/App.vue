<script setup lang="ts">
  import { RouterView } from "vue-router"
  import Header from "@/components/Header.vue"
  import Aside from "@/components/layout/aside/Aside.vue"
  import { inject, onBeforeMount } from "vue"
  import { useUserStore } from "@/store/userStore"
  import { MsalInjectionKey } from "./@symbols"
  import LoginView from "./views/LoginView.vue"
  const { isAuthenticated } = inject(MsalInjectionKey)!
  const { initializeStore } = useUserStore()

  onBeforeMount(async () => {
    if (isAuthenticated) {
      await initializeStore()
    }
  })
</script>

<template>
  <div class="relative bg-zinc-900 text-slate-100 h-screen flex">
    <template v-if="isAuthenticated">
      <Aside />
      <main class="px-9 pt-3 overflow-auto w-full">
        <RouterView></RouterView>
      </main>
    </template>

    <template v-else>
      <LoginView />
    </template>
  </div>
</template>


<script setup lang="ts">
  import { onBeforeRouteUpdate, RouterView } from "vue-router"
  import Header from "@/components/Header.vue"

  import { inject, onBeforeMount, onMounted } from "vue"
  import router from "@/router"
  import { useAuthStore } from "@/store"

  const { initializeUserAccount } = useAuthStore()

  onMounted(async () => {
    await initializeUserAccount()
  })
</script>

<template>
  <div class="h-screen bg-slate-900 text-slate-300">
    <Header />
    <main>
      <RouterView />
    </main>
  </div>
</template>

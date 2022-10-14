<script setup lang="ts">
  import { RouterView } from "vue-router"
  import Header from "@/components/Header.vue"
</script>

<template>
  <div class="relative bg-zinc-900 text-slate-100 min-h-screen">
    <Header />
    <main class="px-9 pt-3">
      <RouterView></RouterView>
    </main>
  </div>
</template>


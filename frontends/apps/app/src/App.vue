<script setup lang="ts">
  import { RouterView } from "vue-router"
  import Header from "@/components/Header.vue"

  import { onBeforeMount } from "vue"
</script>

<template>
  <div class="h-screen bg-slate-900 text-slate-300">
    <Header />
    <main>
      <RouterView />
    </main>
  </div>
</template>


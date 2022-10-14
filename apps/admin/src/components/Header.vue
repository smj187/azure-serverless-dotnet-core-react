<script setup lang="ts">
  import { RouterLink } from "vue-router"
</script>

<template>
  <header
    class="sticky top-0 left-0 w-full h-20 border-b border-slate-200/20 flex items-center justify-center space-x-3"
  >
    <span>Header</span>
    <span>|</span>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/about">About</RouterLink>
  </header>
</template>

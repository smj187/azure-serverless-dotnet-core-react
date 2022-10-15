<script setup lang="ts">
  import { MsalInjectionKey } from "@/@symbols"
  import { inject } from "vue"

  const { account } = inject(MsalInjectionKey)!
</script>

<template>
  <div class="bg-rose-400">HomeView</div>
  <div>
    {{ account }}
  </div>
</template>

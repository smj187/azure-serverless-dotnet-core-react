<script setup lang="ts">
  import { MsalInjectionKey } from "@/@symbols"
  import { inject } from "vue"

  const { redirectToSignIn } = inject(MsalInjectionKey)!
</script>

<template>
  <div class="w-full h-full">
    <div class="ml-64 mt-24">
      <div class="font-bold text-2xl">You need to login:</div>

      <div class="mt-6">
        <button
          @click="redirectToSignIn"
          class="bg-zinc-800/70 px-4 py-2 rounded cursor-pointer hover:bg-zinc-700/50"
        >
          Sign In
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { MsalInjectionKey } from "@/@symbols"
  import { computed, inject } from "vue"

  const { account, redirectToSignOut } = inject(MsalInjectionKey)!

  const lastLogin = computed(() => {
    if (account.value?.idTokenClaims?.iat) {
      const date = new Date(account.value.idTokenClaims.iat * 1000)
      return (
        date.toLocaleDateString("de-DE") +
        " - " +
        date.toLocaleTimeString("de-DE")
      )
    }

    return null
  })

  const name = computed(() => {
    if (account.value?.username) {
      return account.value.username
    }

    return null
  })
</script>

<template>
  <div v-if="account === null">ProfileView</div>
  <div v-else class="flex flex-col space-y-3">
    <div>
      <span class="font-bold pr-3">Account:</span>
      <span>{{ name }}</span>
    </div>
    <div>
      <span class="font-bold pr-3">Login:</span>
      <span>{{ lastLogin }}</span>
    </div>
    <div>
      <button
        @click="redirectToSignOut"
        class="bg-zinc-800/70 px-4 py-2 rounded cursor-pointer hover:bg-zinc-700/50"
      >
        Sign Out
      </button>
    </div>
  </div>
</template>

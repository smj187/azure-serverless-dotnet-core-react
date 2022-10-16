<script setup lang="ts">
  import { useUserStore } from "@/store/userStore"
  import { storeToRefs } from "pinia"

  const { accountTiers } = storeToRefs(useUserStore())
</script>

<template>
  <div>Available AccountTiers</div>
  <template v-for="accountTier in accountTiers" :key="accountTier.value">
    <div class="flex space-x-2">
      <div>
        {{ accountTier.value }}
      </div>
      <div>
        {{ accountTier.description }}
      </div>
    </div>
  </template>
</template>

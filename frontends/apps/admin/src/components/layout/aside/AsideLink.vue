<script setup lang="ts">
  import { RouterLink } from "vue-router"

  interface Props {
    isMinimal: boolean
    route: string
    label: string
  }

  const props = defineProps<Props>()
</script>

<template>
  <RouterLink v-slot="{ isActive }" :to="props.route">
    <div
      :class="
        isActive
          ? 'opacity-100'
          : 'opacity-50 hover:opacity-100 transition-opacity ease-in-out duration-200'
      "
      class="flex items-center w-full h-12"
    >
      <div class="w-20 grid place-content-center">
        <slot></slot>
      </div>
      <div v-if="!props.isMinimal">{{ props.label }}</div>
    </div>
  </RouterLink>
</template>

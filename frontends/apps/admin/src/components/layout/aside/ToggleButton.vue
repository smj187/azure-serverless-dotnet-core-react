<script setup lang="ts">
  interface Props {
    isMinimal: boolean
  }

  interface Emit {
    (event: "update:value", newValue: boolean): void
  }

  const props = defineProps<Props>()
  const emit = defineEmits<Emit>()
</script>

<template>
  <button
    @click="emit('update:value', !props.isMinimal)"
    class="w-10 h-10 opacity-50 hover:opacity-100 transition-opacity ease-in-out duration-200 grid place-content-center rounded"
  >
    <svg
      v-if="!props.isMinimal"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke-width="1.5"
      stroke="currentColor"
      class="w-6 h-6"
    >
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="M18.75 19.5l-7.5-7.5 7.5-7.5m-6 15L5.25 12l7.5-7.5"
      />
    </svg>

    <svg
      v-else
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke-width="1.5"
      stroke="currentColor"
      class="w-6 h-6"
    >
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="M11.25 4.5l7.5 7.5-7.5 7.5m-6-15l7.5 7.5-7.5 7.5"
      />
    </svg>
  </button>
</template>

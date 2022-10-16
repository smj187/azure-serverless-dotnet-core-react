<script setup lang="ts">
  import BaseDialog from "@/components/dialogs/BaseDialog.vue"
  import { ref } from "vue"

  const isOpen = ref(false)
</script>

<template>
  <div @click="isOpen = !isOpen">
    <slot></slot>
  </div>

  <BaseDialog :value="isOpen" @update:value="newValue => (isOpen = newValue)">
    <div class="flex flex-col">hello</div>
  </BaseDialog>
</template>

<script setup lang="ts">
  import {
    computed,
    ComputedRef,
    onBeforeMount,
    onMounted,
    onUnmounted,
    ref,
    watch
  } from "vue"
  import { useRoute } from "vue-router"
  import { useVoiceStore } from "@/store/voiceStore"
  import { storeToRefs } from "pinia"
  import { watchDebounced } from "@vueuse/core"

  const { params } = useRoute()
  const { findVoiceAsync, patchVoiceNameAsync } = useVoiceStore()
  const { voiceDetails } = storeToRefs(useVoiceStore())
  const voiceId = ref(params.voiceId as string)

  const name = ref("")

  onBeforeMount(async () => {
    await findVoiceAsync(voiceId.value)

    name.value = voiceDetails.value?.displayName ?? "null"
  })

  watchDebounced(
    name,
    () => {
      if (voiceDetails.value) {
        if (name.value !== voiceDetails.value.displayName) {
          patchVoiceNameAsync(voiceDetails.value.id, name.value)
        }
      }
    },
    { debounce: 1000, maxWait: 5000 }
  )
</script>

<template>
  <div v-if="voiceDetails === null">VoiceDetails {{ voiceId }}</div>
  <div v-else>
    {{ voiceDetails }}
    <div>
      Name:
      <input v-model="name" class="px-3 py-2 bg-zinc-700/50 rounded" />
    </div>
  </div>
</template>

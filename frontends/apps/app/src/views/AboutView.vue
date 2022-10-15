<script setup lang="ts">
  import { MsalInjectionKey } from "@/@symbols"
  import { inject, onMounted } from "vue"
  import { getWeather } from "@/http/weather/weatherService"

  async function test() {
    const data = await getWeather()
    console.log(data)
  }

  onMounted(async () => {})
</script>

<template>
  <div class="bg-rose-400">AboutView</div>
  <button @click="test">click</button>
</template>

<script setup lang="ts">
  import { onMounted } from "vue"
  import { MsalInstance } from "@/azure/b2cClient"

  onMounted(async () => {
    const accessToken = await MsalInstance.getToken()

    const headers = new Headers()
    const bearer = `Bearer ${accessToken}`
    headers.append("Authorization", bearer)
    const options = {
      method: "GET",
      headers: headers
    }
    const data = await fetch("https://localhost:8000/WeatherForecast", options)

    console.log(await data.json())
  })
</script>

<template>
  <div class="bg-rose-400">AboutView</div>
</template>

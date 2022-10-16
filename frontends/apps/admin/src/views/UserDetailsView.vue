<script setup lang="ts">
  import { computed, ComputedRef, onBeforeMount, ref, watch } from "vue"
  import { useRoute } from "vue-router"
  import { useUserStore } from "@/store/userStore"
  import { storeToRefs } from "pinia"
  import BarChart, { ChartData } from "@/components/charts/BarChart"
  import AccountTiersList from "@/components/AccountTiersList.vue"

  const { findUserAsync, patchAccountTier } = useUserStore()
  const { userDetails, accountTiers } = storeToRefs(useUserStore())
  const { params } = useRoute()

  const userId = ref(params.userId as string)

  onBeforeMount(async () => {
    await findUserAsync(userId.value)
  })

  const data = computed(() => {
    if (userDetails.value === null) {
      return {
        labels: [],
        datasets: []
      }
    }

    const localData: { date: Date; usage: number }[] = []

    userDetails.value.creditHistory.forEach(el => {
      const usage = el.entries.reduce((a, b) => a + b.credits, 0)

      localData.push({ date: el.date, usage })
    })

    const labels = localData.map(r =>
      new Date(r.date).toLocaleDateString("de-DE")
    )
    const datasets = localData.map(x => x.usage)

    return {
      labels,
      datasets: [{ data: datasets }]
    }
  })
</script>

<template>
  <div v-if="userDetails === null">UserDetailsView</div>
  <div v-else>
    <div class="flex items-center space-x-2">
      <RouterLink to="/users">
        <button
          class="bg-zinc-800/70 w-8 h-8 grid place-content-center hover:bg-zinc-700/50 rounded"
        >
          <svg
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
              d="M10.5 19.5L3 12m0 0l7.5-7.5M3 12h18"
            />
          </svg>
        </button>
      </RouterLink>
      <div>UserDetailsView</div>
    </div>
    <BarChart :chart-data="data" />
    <div>
      Start:
      {{ new Date(userDetails.historyStartDate).toLocaleDateString("de-DE") }}
    </div>
    <div>
      End:
      {{ new Date(userDetails.historyResetDate).toLocaleDateString("de-DE") }}
    </div>

    <div class="mt-16">
      <div class="text-xl font-bold">User's current Account Tier:</div>
      <div class="py-3 opacity-60 italic">
        {{ userDetails.accountTier.description }}
      </div>
      <div class="flex flex-col space-y-3 w-48">
        <template v-for="accountTier in accountTiers" :key="accountTier.value">
          <button
            @click="() => patchAccountTier(accountTier.value)"
            class="bg-zinc-800/70 px-4 py-2 rounded cursor-pointer hover:bg-zinc-700/50"
          >
            change to {{ accountTier.description }}
          </button>
        </template>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { useVoiceStore } from "@/store/voiceStore"
  import { storeToRefs } from "pinia"
  const { voices } = storeToRefs(useVoiceStore())
</script>

<template>
  <div class="max-w-7xl">
    <table class="table-auto w-full rounded-t-md overflow-hidden">
      <thead class="bg-zinc-700/30 h-12">
        <tr class="text-left text-md">
          <th class="px-3">Voice Id</th>
          <th class="px-3">Name</th>
          <th class="px-3">Locale</th>
          <th class="px-3">Gender</th>
          <th class="px-3">Type</th>
          <th class="px-3">Actions</th>
        </tr>
      </thead>
      <tbody>
        <template v-for="voice in voices" :key="voice.id">
          <tr class="border-b border-slate-200/20 h-12 hover:bg-zinc-700/50">
            <td class="w-96 px-3">{{ voice.id }}</td>
            <td class="w-32 px-3">
              {{ voice.displayName }}
            </td>
            <td class="w-40 px-3">
              {{ voice.locale }}
            </td>
            <td class="w-32 px-3">{{ voice.gender }}</td>
            <td class="w-32 px-3">{{ voice.voiceType.description }}</td>
            <td class="w-32 px-3">
              <RouterLink :to="`/voices/${voice.id}`">
                <button
                  class="w-10 h-10 rounded grid place-content-center hover:bg-zinc-700/50"
                >
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke-width="1.5"
                    stroke="currentColor"
                    class="w-5 h-5"
                  >
                    <path
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      d="M3 13.125C3 12.504 3.504 12 4.125 12h2.25c.621 0 1.125.504 1.125 1.125v6.75C7.5 20.496 6.996 21 6.375 21h-2.25A1.125 1.125 0 013 19.875v-6.75zM9.75 8.625c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125v11.25c0 .621-.504 1.125-1.125 1.125h-2.25a1.125 1.125 0 01-1.125-1.125V8.625zM16.5 4.125c0-.621.504-1.125 1.125-1.125h2.25C20.496 3 21 3.504 21 4.125v15.75c0 .621-.504 1.125-1.125 1.125h-2.25a1.125 1.125 0 01-1.125-1.125V4.125z"
                    />
                  </svg>
                </button>
              </RouterLink>
            </td>
          </tr>
        </template>
      </tbody>
    </table>
  </div>
</template>

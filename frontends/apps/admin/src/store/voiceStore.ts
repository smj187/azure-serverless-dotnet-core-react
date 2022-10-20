import {
  findVoice,
  listVoices,
  patchVoiceName,
  Voice
} from "@/http/voices/voiceService"
import { defineStore } from "pinia"

type State = {
  voices: Array<Voice>
  voiceDetails: Voice | null
}

export const useVoiceStore = defineStore("voice-store", {
  state: (): State => ({
    voices: [],
    voiceDetails: null
  }),

  getters: {},

  actions: {
    async initializeVoiceStore() {
      this.voices = await listVoices()
    },

    async findVoiceAsync(voiceId: string) {
      this.voiceDetails = null
      this.voiceDetails = await findVoice(voiceId)
    },

    async patchVoiceNameAsync(voiceId: string, name: string) {
      this.voiceDetails = await patchVoiceName(voiceId, name)
    }
  }
})

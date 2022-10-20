import { HttpClient } from "../base/baseHttpClient"

export interface AvatarImage {
  url: string
  contentType: string
  name: string
  size: number
}

export interface PreviewAudio {
  url: string
  contentType: string
  name: string
  size: number
}

export interface VoiceProvider {
  value: number
  description: string
}

export interface VoiceType {
  value: number
  description: string
}

export interface AwsVoiceConfig {
  specialStyles: string[]
  engines: string[]
  defaultEngine: string
}

export interface GoogleVoiceConfig {
  languageCodes: string[]
  naturalSampleRateHertz: number
  defaultLanguageCode: string
}

export interface AzureVoiceConfig {
  sampleRateHertz: number
  voiceType: string
  wordsPerMinute: number
  styleList: string[]
  rolePlayList?: any
  isHighQuality48K: boolean
}

export interface Voice {
  displayName: string
  internalName: string
  locale: string
  gender: string
  avatarImage: AvatarImage
  previewAudio: PreviewAudio
  voiceProvider: VoiceProvider
  voiceType: VoiceType
  awsVoiceConfig: AwsVoiceConfig
  googleVoiceConfig: GoogleVoiceConfig
  azureVoiceConfig: AzureVoiceConfig
  isAvailable: boolean
  id: string
  createdAt: Date
  modifiedAt?: any
}

interface PatchVoiceNameRequest {
  voiceId: string
  name: string
}

export const listVoices = async () => {
  const r = await HttpClient.get<Array<Voice>>(`/co/api/v1/voices/list`)
  return r.data
}

export const findVoice = async (voiceId: string) => {
  const r = await HttpClient.get<Voice>(`/co/api/v1/voices/${voiceId}`)
  return r.data
}

export const patchVoiceName = async (voiceId: string, name: string) => {
  const r = await HttpClient.patch<Voice>(
    `/co/api/v1/voices/name`,
    JSON.stringify({
      name,
      voiceId
    } as PatchVoiceNameRequest)
  )

  return r.data
}

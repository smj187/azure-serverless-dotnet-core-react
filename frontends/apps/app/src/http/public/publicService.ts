import { HttpClient } from "../http"

export interface Public {
  message: string
}

export const getPublic = async () => {
  const res = await HttpClient.get<Public>("/public")
  return res.data
}

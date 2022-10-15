import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios"
import { B2cInstance, scopes } from "@/config/B2cInstance"

const headers: Readonly<Record<string, string | boolean>> = {
  Accept: "application/json",
  "Content-Type": "application/json; charset=utf-8"
}

export const HttpClient = axios.create({
  baseURL: "https://localhost:8000",
  headers
})

const injectToken = async (config: AxiosRequestConfig) => {
  try {
    if (B2cInstance.getActiveAccount()) {
      const { accessToken } = await B2cInstance.acquireTokenSilent({
        scopes: [...scopes]
      })
      config.headers!.Authorization = `Bearer ${accessToken}`
    }

    return config
  } catch (error) {
    console.error(error)
    throw new Error("err")
  }
}

HttpClient.interceptors.request.use(injectToken, err => Promise.reject(err))

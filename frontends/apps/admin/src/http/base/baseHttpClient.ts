import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios"
import { AdInstance, scopes } from "@/config/AdInstance"

const headers: Readonly<Record<string, string | boolean>> = {
  Accept: "application/json",
  "Content-Type": "application/json; charset=utf-8"
}

export const HttpClient = axios.create({
  baseURL: "https://localhost:7200",
  headers
})

const injectToken = async (config: AxiosRequestConfig) => {
  try {
    if (AdInstance.getActiveAccount()) {
      const { accessToken } = await AdInstance.acquireTokenSilent({
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

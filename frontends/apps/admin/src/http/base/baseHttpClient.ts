import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios"
import { AdInstance, scopes } from "@/config/AdInstance"

const headers: Readonly<Record<string, string | boolean>> = {
  Accept: "application/json",
  "Content-Type": "application/json; charset=utf-8"
}

// const data = await fetch(
//   "https://localhost:10000/id/api/v1/users/list-tiers",
//   {
//     headers: {
//       Accept: "application/json",
//       "Content-Type": "application/json; charset=utf-8",
//       "Access-Control-Allow-Origin": "*",
//       "Access-Control-Allow-Methods": "POST, PATCH, DELETE, GET, OPTIONS",
//       "Access-Control-Request-Method": "*",
//       "Access-Control-Allow-Headers":
//         "Origin, X-Requested-With, Content-Type, Accept, Authorization"
//     },
//     mode: "cors"
//   }
// )

export const HttpClient = axios.create({
  baseURL: "https://localhost:10000",
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

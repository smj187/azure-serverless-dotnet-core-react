import { HttpClient } from "../http"

export interface Weather {
  date: Date
  temperatureC: number
  temperatureF: number
  summary: string
}

export const getWeather = async () => {
  const response = await HttpClient.get<Weather[]>("/WeatherForecast")
  return response.data
}

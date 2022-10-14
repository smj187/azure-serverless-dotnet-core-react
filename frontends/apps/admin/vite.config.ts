import { defineConfig, loadEnv } from "vite"
import vue from "@vitejs/plugin-vue"
import { resolve } from "path"

export default defineConfig(({ mode }) => {
  process.env = { ...process.env, ...loadEnv(mode, process.cwd()) }

  return {
    plugins: [vue()],
    resolve: {
      alias: {
        "@": resolve(__dirname, "src")
      }
    },
    server: {
      host: "0.0.0.0",
      port: 3001
    }
  }
})


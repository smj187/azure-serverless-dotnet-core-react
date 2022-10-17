import { defineConfig, loadEnv } from "vite"
import vue from "@vitejs/plugin-vue"
import { resolve } from "path"
import mkcert from "vite-plugin-mkcert"

export default defineConfig(({ mode }) => {
  process.env = { ...process.env, ...loadEnv(mode, process.cwd()) }

  return {
    plugins: [vue(), mkcert()],
    resolve: {
      alias: {
        "@": resolve(__dirname, "src")
      }
    },
    server: {
      host: "0.0.0.0",
      port: 3001,
      https: true
    }
  }
})


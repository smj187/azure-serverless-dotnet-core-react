import { defineConfig, loadEnv } from "vite"
import vue from "@vitejs/plugin-vue"
import { resolve } from "path"
import fs from "fs"

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
      port: 3001,
      https: {
        key: fs.readFileSync("./certificates/server.key"),
        cert: fs.readFileSync("./certificates/server.crt")
      }
    }
  }
})


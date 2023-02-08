import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { VitePWA } from "vite-plugin-pwa";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    VitePWA(
      // {
      // manifest: { 
      //   icons: [
      //     {
      //       src: "/icons/512.png",
      //       sizes: "512x512",
      //       type: "image/png",
      //       purpose: "any maskable"
      //     }
      //   ]
      // },
      // workbox: {
      //   runtimeCaching: [{
      //     urlPattern: ({ url }) => {
      //       return url.pathname.startsWith("/api");
      //     },
      //     handler: "CacheFirst" as const,
      //     options: {
      //       cacheName: "api-cache",
      //       cacheableResponse: {
      //         statuses: [0, 200]
      //       }
      //     }
      //   }
      //]
      //}
    //}
    )
  ],
  build: {
    outDir: '../wwwroot/',
    emptyOutDir: true
  },
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  }
})

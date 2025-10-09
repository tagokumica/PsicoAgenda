import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig(({ mode }) => ({
  plugins: [react()],
  server: {
    host: true,        
    port: 5173,
    strictPort: true,
    hmr: {
      clientPort: 5173 
    },
    watch: {
      usePolling: true 
    },
    proxy: {
      '/api': {
        target: 'http://api:8080',
        changeOrigin: true
      }
    }
  },
  preview: {
    port: 5173,
    host: true
  },
  build: {
    outDir: 'dist'
  }
}))
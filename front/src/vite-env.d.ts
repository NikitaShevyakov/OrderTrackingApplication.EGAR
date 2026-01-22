/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_OTA_URL: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
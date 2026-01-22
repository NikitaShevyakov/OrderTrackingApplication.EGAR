interface ImportMetaEnv {
  readonly VITE_APP_NAME: string
  readonly VITE_API_AUTH_URL: string
  readonly VITE_API_DATA_URL: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
import { combineSlices, configureStore } from "@reduxjs/toolkit"
import { setupListeners } from "@reduxjs/toolkit/query"
import { ordersApi } from "../api/ordersApi"
import ordersReducer from "./orderSlice"; 

const rootReducer = combineSlices({
  [ordersApi.reducerPath]: ordersApi.reducer,
  orders: ordersReducer,
})

export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(ordersApi.middleware),
})

setupListeners(store.dispatch)

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
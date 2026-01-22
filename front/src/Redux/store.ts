// import type { Action, ThunkAction } from "@reduxjs/toolkit"
import { combineSlices, configureStore } from "@reduxjs/toolkit"
import { setupListeners } from "@reduxjs/toolkit/query"
import { ordersApi } from "../api/ordersApi"
import ordersReducer from "./orderSlice"; 

// export type RootState = ReturnType<typeof rootReducer>

// export const makeStore = (preloadedState?: Partial<RootState>) => {
//   const store = configureStore({
//     reducer: rootReducer,
//     middleware: getDefaultMiddleware => {
//       return getDefaultMiddleware()
//         .concat(ordersApi.middleware)
//     },
//     preloadedState,
//   })

//   setupListeners(store.dispatch)
//   return store
// }

// export const store = makeStore()

// export type AppStore = typeof store
// export type AppDispatch = AppStore["dispatch"]
// export type AppThunk<ThunkReturnType = void> = ThunkAction<
//   ThunkReturnType,
//   RootState,
//   unknown,
//   Action
// >


// import { configureStore } from "@reduxjs/toolkit"
// import { setupListeners } from "@reduxjs/toolkit/query"
// import { ordersApi } from "../api/ordersApi"
// import ordersReducer from "../Redux/orderSlice"

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
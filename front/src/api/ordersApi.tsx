import { createApi} from "@reduxjs/toolkit/query/react";
// import { buildQueryParams } from "../utils/buildQueryParams";
import { baseQueryWithNiceError } from './baseQuery';
import { OrdersResponse, OrdersParams } from "./types/orders";
import { CreateOrderData, Order } from "../types/order";

const BASE_URL = import.meta.env.VITE_API_OTA_URL;

export const ordersApi = createApi({
  reducerPath: "ordersApi",
  baseQuery: baseQueryWithNiceError(BASE_URL),  
  endpoints: (builder) => ({
    getOrders: builder.query<OrdersResponse, OrdersParams>({
      query: (params) => ({
        url: '/orders',
        params: {
          page: params.page || 1,
          limit: params.limit || 10
          // ...(params.status && { status: params.status }),
          // ...(params.startDate && { startDate: params.startDate }),
          // ...(params.endDate && { endDate: params.endDate }),
        },
      })
    }),

    createOrder: builder.mutation<Order, CreateOrderData>({
      query: (body) => ({
        url: '/orders',
        method: 'POST',
        body: body
      })    
    }),
  }),
});

export const {
  useGetOrdersQuery,
  useCreateOrderMutation
} = ordersApi;
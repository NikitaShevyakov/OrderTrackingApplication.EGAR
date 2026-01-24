import { createApi} from "@reduxjs/toolkit/query/react";
// import { buildQueryParams } from "../utils/buildQueryParams";
import { baseQueryWithNiceError } from './baseQuery';
import { OrdersResponse, OrdersParams } from "./types/orders";
import { CreateOrderData, Order } from "../types/order";
import { OrderStatus } from "../types/orderStatus";

const BASE_URL = import.meta.env.VITE_API_OTA_URL;

export const ordersApi = createApi({
  reducerPath: "ordersApi",
  baseQuery: baseQueryWithNiceError(BASE_URL),  
  endpoints: (builder) => ({

    getOrderById: builder.query<Order, number>({
      query: (id) => `orders/${id}`
    }),

    getOrders: builder.query<OrdersResponse, OrdersParams>({
      query: (params) => ({
        url: '/orders',
        params: {
          page: params.page || 1,
          limit: params.limit || 10
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

    changeOrderStatus: builder.mutation<Order, { id: number; status: OrderStatus }>({
      query: ({id, status}) => ({
        url: `/orders/${id}/status`,
        method: 'POST',
        body: { id, status}
      })    
    })
  }),
});

export const {
  useGetOrderByIdQuery,
  useGetOrdersQuery,
  useCreateOrderMutation,
  useChangeOrderStatusMutation
} = ordersApi;
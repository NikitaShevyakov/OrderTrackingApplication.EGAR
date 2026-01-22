import { Order } from "../../types/order";

export interface OrdersResponse {
  orders: Order[];
  total: number;
  page: number;
  totalPages: number;
}

export interface OrdersParams {
  page?: number;
  limit?: number;
  // status?: OrderStatus;
  // startDate?: string;
  // endDate?: string;
}
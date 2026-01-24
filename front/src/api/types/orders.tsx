import { Order } from "../../types/order";

export interface OrdersResponse {
  items: Order[];
  total: number;
  page: number;
  pageSize: number;
}

export interface OrdersParams {
  page?: number;
  limit?: number;
  // status?: OrderStatus;
  // startDate?: string;
  // endDate?: string;
}
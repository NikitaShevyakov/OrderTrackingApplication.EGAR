import { OrderStatus } from "./orderStatus";

export interface Order {
  id: number;
  orderNumber: string;
  description: string;
  status: OrderStatus;
  createdAt: string;
  updatedAt: string;
}

export interface CreateOrderData {
  orderNumber: string;
  description: string;
}
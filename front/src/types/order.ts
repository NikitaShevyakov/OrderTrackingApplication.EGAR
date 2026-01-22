export enum OrderStatus {
  Created = 0,
  Sent = 1,
  Delivered = 2,
  Cancelled = 3
}

export const getOrderStatusLabel = (status: OrderStatus): string => {
  switch (status) {
    case OrderStatus.Created: return 'Создан';
    case OrderStatus.Sent: return 'Отправлен';
    case OrderStatus.Delivered: return 'Доставлен';
    case OrderStatus.Cancelled: return 'Отменён';
    default: return 'Неизвестный статус';
  }
};

export interface Order {
  id: number;
  orderNumber: string;
  description: string;
  status: OrderStatus;
  createdAt: string;
}

export interface CreateOrderData {
  orderNumber: string;
  description: string;
}
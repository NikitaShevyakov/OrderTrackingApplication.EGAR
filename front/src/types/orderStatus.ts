import { LabelValueItem } from "./labelValueItem";
import { Order } from "./order";

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

export const getStatusColor = (status: Order['status']) => {
  switch (status) {
    case OrderStatus.Sent: return 'secondary';
    case OrderStatus.Delivered: return 'success';
    case OrderStatus.Created: return 'info';
    case OrderStatus.Cancelled: return 'error';
  }
};

export const statusItems: LabelValueItem[] = Object.entries(OrderStatus)
  .filter(([key, value]) => typeof value === 'number')
  .map(([key, value]) => ({
    value: value as number,
    label: getOrderStatusLabel(value as OrderStatus),
  }));
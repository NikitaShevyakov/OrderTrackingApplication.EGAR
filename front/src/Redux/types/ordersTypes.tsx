export interface OrdersFilters {
  page: number;
  limit: number;
}

export interface OrdersState {
  filters: OrdersFilters;
}
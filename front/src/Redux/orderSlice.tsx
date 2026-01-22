import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { OrdersState, OrdersFilters } from "./types/ordersTypes";

const initialState: OrdersState = {
  filters: {
    page:  1,
    limit: 15
  }
};

const ordersSlice = createSlice({
  name: "orders",
  initialState: initialState,
  reducers: {
    setPage(state, action: PayloadAction<number>) {
      state.filters.page = action.payload;
    },
    setLimit(state, action: PayloadAction<number>) {
      state.filters.limit = action.payload;
    },
    setFilters: (state, action: PayloadAction<Partial<OrdersFilters>>) => {
      state.filters = { ...state.filters, ...action.payload };
    }
  }
});

export const {
  setPage,
  setLimit
} = ordersSlice.actions;

export default ordersSlice.reducer;
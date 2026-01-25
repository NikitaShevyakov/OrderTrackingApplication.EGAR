import { useCallback, useEffect, useMemo, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState } from "../../Redux/store";
import { CreateOrderData } from "../../types/order";
import { useCreateOrderMutation, useDeleteOrderMutation, useGetOrdersQuery } from "../../api/ordersApi";
import { OrdersParams } from "../../api/types/orders";
import { setPage, setLimit, resetFilters } from "../../Redux/orderSlice";
import useAutoDismissAlert from "../../hooks/useAutoDismissAlert";

export default function useOrdersPage() {
  const dispatch = useDispatch();
  const filters = useSelector((s: RootState) => s.orders.filters);
  const params: OrdersParams = useMemo(() => ({ page: filters.page, limit: filters.limit }), [filters.page, filters.limit]);

  const { data: orders, isError: isErrorFetchingOrders, isLoading } = useGetOrdersQuery(params);
  const [createOrder] = useCreateOrderMutation();
  const [deleteOrder] = useDeleteOrderMutation();

  const [openModal, setOpenModal] = useState(false);
  const { alert, showAlert } = useAutoDismissAlert(5000);

  useEffect(() => {
    if (isErrorFetchingOrders) {
      showAlert("error", "Ошибка при получении заказов.");
    }
  }, [isErrorFetchingOrders, showAlert]);

const performWithReset = useCallback(
  async <T>(mutationCall: () => Promise<T>, successMsg: string, opts?: { closeModal?: boolean }) => {
    const prevPage = filters.page;
    const prevLimit = filters.limit;

    dispatch(resetFilters());
    if (opts?.closeModal) setOpenModal(false);

    try {
      await mutationCall();
      showAlert("success", successMsg);
    } catch (e) {
      dispatch(setLimit(prevLimit));
      dispatch(setPage(prevPage));
      console.error(e);
      showAlert("error", "Ошибка при выполнении операции.");
      throw e;
    }
  },
  [dispatch, filters.page, filters.limit, showAlert]
);

const handleCreateNewOrder = useCallback(
  (data: CreateOrderData) => performWithReset(() => createOrder(data).unwrap(), "Заказ успешно создан!", { closeModal: true }),
  [createOrder, performWithReset]
);

const handleDeleteOrder = useCallback(
  (id: number) => performWithReset(() => deleteOrder(id).unwrap(), "Заказ удалён"),
  [deleteOrder, performWithReset]
);

  const onPageChange = useCallback((page: number) => dispatch(setPage(page)), [dispatch]);
  const onRowsPerPageChange = useCallback((limit: number) => { dispatch(setLimit(limit)); dispatch(setPage(1)); }, [dispatch]);

  return {
    rows: orders?.items ?? [],
    total: orders?.total ?? 0,
    isLoading,
    openModal,
    setOpenModal,
    handleCreateNewOrder,
    alertState: alert,
    onPageChange,
    onRowsPerPageChange,
    filters,
    handleDeleteOrder
  } as const;
}
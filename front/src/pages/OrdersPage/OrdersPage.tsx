import { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState } from "../../Redux/store"; 
import { CreateOrderData } from "../../types/order";
import { useCreateOrderMutation, useGetOrdersQuery } from "../../api/ordersApi";
import { OrdersParams } from "../../api/types/orders";
import OrdersTable from "./OrdersTable";
import { setPage, setLimit } from "../../Redux/orderSlice";
import "./OrdersPage.scss";

import { Add as AddIcon } from '@mui/icons-material';
import { Button, Alert } from '@mui/material';
import CreateOrderModal from "./CreateOrderModal";

const OrdersPage = () => {
  const dispatch = useDispatch();

  const filters = useSelector((state: RootState) => state.orders.filters);
  const [openModal, setOpenModal] = useState<boolean>(false);

  const params: OrdersParams = {
    page: filters.page,
    limit: filters.limit
  };

  const { 
    data: orders,
    isError: isErrorFetchingOrders,
    refetch: refetchOrders
  } = useGetOrdersQuery(params);
  const [createOrder, { isError: isCreateOrderError, isSuccess: isCreateOrderSuccess }] = useCreateOrderMutation();

  const rows = orders?.orders ?? [];
  const total = orders?.total ?? 0;

  const handleCloseModal = () =>{
    setOpenModal(false);
  }

  useEffect(() => {
    if (isCreateOrderSuccess) {
      refetchOrders();
      handleCloseModal();
    }
  }, [isCreateOrderSuccess, refetchOrders]);

  const handleCreateNewOrder = async  (data:CreateOrderData) =>{
      await createOrder(data).unwrap()
        .catch((error)=> {
          console.error("Ошибка при создании заказа:", error);
        });
  }

  const [alertState, setAlertState] = useState<{
    show: boolean;
    severity: 'success' | 'error' ;
    text: string;
  }>({ show: false, severity: 'success', text: '', });

  useEffect(() => {
    let timer: NodeJS.Timeout;
   
    if (alertState.show) {
      timer = setTimeout(() => {
        setAlertState(prev => ({ ...prev, show: false }));
      }, 5000);
    }

    return () => {
      if (timer) clearTimeout(timer);
    };
  }, [alertState.show]);

  useEffect(() => {
    switch (true) {
      case isCreateOrderSuccess: setAlertState({ show: true, severity: 'success', text: 'Заказ успешно создан!', }); break;
      case isCreateOrderError: setAlertState({ show: true, severity: 'error', text: 'Ошибка при создании заказа.', }); break;
      case isErrorFetchingOrders: setAlertState({ show: true, severity: 'error', text: 'Ошибка при получении заказов.',}); break; 
      default: break;
    }
  }, [isCreateOrderSuccess, isCreateOrderError, isErrorFetchingOrders]);

  return (
    <div>
      <div className="btns-wrapp">      
        <Button variant="outlined" startIcon={<AddIcon />} onClick={() =>{setOpenModal(true);}} >Создать заказ</Button>  
        <CreateOrderModal
          isOpen={openModal}
          onClose={handleCloseModal}
          onSubmit={handleCreateNewOrder}
        />
      </div>

      {alertState.show && <Alert sx={{ mt: 1, mb: 1 }} severity={alertState.severity}> {alertState.text} </Alert>}

      <OrdersTable
        rows={rows}
        total={total}
        filters={filters}
        // onSort={handleSort}
        onPageChange={(page:number) => dispatch(setPage(page))}
        onRowsPerPageChange={(limit:number) => {
          dispatch(setLimit(limit));
          dispatch(setPage(1));
        }}
      />
    </div>
  );
}

export default OrdersPage;
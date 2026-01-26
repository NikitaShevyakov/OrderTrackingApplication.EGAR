import { useEffect, useState } from "react";
import { Box, Card, Typography } from "@mui/material";
import {
  useChangeOrderStatusMutation,
  useGetOrderByIdQuery
} from "../../api/ordersApi";
import { useParams } from "react-router-dom";
import SimpleSelect from "../../components/SimpleSelect";
import { OrderStatus, statusItems } from "../../types/orderStatus";
import { Order, OrderStatusChangedEvent } from "../../types/order";
import { useOrderStatusSSE } from "../../api/useOrderStatusSSE";
import OrderStatusHistory from "./OrderStatusHistory";
import OrderMeta from "./OrderMeta";
import OrderHeader from "./OrderHeader";

const OrderPage = () => {
  const { id } = useParams<{ id: string }>();
  const [changeOrderStatus] = useChangeOrderStatusMutation();
  const [events, setEvents] = useState<OrderStatusChangedEvent[]>([]);

  const orderId = id ? parseInt(id, 10) : null;
  const isValidId = orderId && !isNaN(orderId) && orderId > 0;
  const { data: order, isLoading } = useGetOrderByIdQuery(orderId!, { skip: !isValidId, });
  const stream = useOrderStatusSSE(orderId);

  useEffect(() => {
    if (stream) {
      setEvents(prev => [stream, ...prev]);
    }
  }, [stream]);

  if (isLoading) return <div>Загрузка...</div>;
  if (!order) return <div>Данные отсутствуют!</div>;

  const actualStatus = stream?.NewStatus ?? order.status;
  const actualChangedAt = stream?.ChangedAt ?? order.updatedAt;

  const handlerChangeOrderStatus = async (status: string | number) => {
    const orderStatus = typeof status === 'string' ? (parseInt(status, 10) as OrderStatus) : (status as OrderStatus);
    if (!isValidId) {
      console.error('Неверный id заказа, пропускаю изменение статуса');
      return;
    }
    await changeOrderStatus({ id: orderId, status: orderStatus }).unwrap()
      .catch((error) => {
        console.error("Ошибка при смене статуса", error);
      });
  }

  return (
    <Card sx={{ p: 2, mb: 2 }}>
      <OrderHeader order={order as Order} status={actualStatus} />      
      <Typography variant="body1" mb={2}>{order.description}</Typography>
      <Box>
        <SimpleSelect label="Выбор статуса" options={statusItems} value={actualStatus} onChange={handlerChangeOrderStatus} />
      </Box>
      <Box mt={2}>
        <OrderMeta createdAt={order.createdAt} changedAt={actualChangedAt} />
      </Box>
      <Box mt={3}>
        <Typography variant="subtitle1">История изменений статуса</Typography>
        <OrderStatusHistory events={events} />
      </Box>
    </Card>
  );
}

export default OrderPage;
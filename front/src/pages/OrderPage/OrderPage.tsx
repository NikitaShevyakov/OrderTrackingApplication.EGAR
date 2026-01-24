import { Box, Card, Chip, Typography } from "@mui/material";
import { useChangeOrderStatusMutation, useGetOrderByIdQuery } from "../../api/ordersApi";
import { useParams } from "react-router-dom";
import { getOrderStatusLabel, getStatusColor, OrderStatus } from "../../types/orderStatus";
import { formatDateTime } from '../../utils/dateFormatter';
import SimpleSelect from "../../components/SimpleSelect";
import { statusItems } from "../../types/orderStatus";

const OrderPage = () => {
  const {id} = useParams<{id:string}>();
  const [changeOrderStatus, { isError, isSuccess }] = useChangeOrderStatusMutation();

  const orderId = id ? parseInt(id, 10) : null;
  const isValidId = orderId && !isNaN(orderId) && orderId > 0;
  const { data: order, isLoading } = useGetOrderByIdQuery(orderId!, {skip: !isValidId,});

  if(!order) return <div>Данные отсутствуют!</div>;  

  if(isLoading) return <div>Загрузка...</div>;  


  const handlerChangeOrderStatus = async (status: string) => {
    const orderStatus = parseInt(status) as OrderStatus;
    console.log(orderId);
    await changeOrderStatus({ id:orderId, status:orderStatus}).unwrap()
        .catch((error)=> {
          console.error("Ошибка при смене статуса", error);
        });
  }

  return (
    <Card sx={{ p: 2, mb: 2 }}>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
        <Typography variant="h6">Заказ #{order.orderNumber}</Typography>
        <Chip label={getOrderStatusLabel(order.status)} color={getStatusColor(order.status)} size="small" />
      </Box>      
      <Typography variant="body1" mb={2}>{order.description}</Typography>
      <Box>
        <SimpleSelect label="Выбор статуса" options={statusItems} value={order.status} onChange={handlerChangeOrderStatus}/>
      </Box>
      <Box display="flex" gap={3}>
        <Box>
          <Typography variant="caption" color="textSecondary">Создан:</Typography>
          <Typography variant="body2">{formatDateTime(order.createdAt)}</Typography>
        </Box>
        <Box>
          <Typography variant="caption" color="textSecondary">Изменен:</Typography>
          <Typography variant="body2">{formatDateTime(order.updatedAt)}</Typography>
        </Box>
      </Box>
    </Card>
  );
}

export default OrderPage;
import { Box, Chip, Typography } from "@mui/material";
import { getOrderStatusLabel, getStatusColor } from "../../types/orderStatus";
import { Order } from "../../types/order";

type Props = { order: Order; status: number };

export default function OrderHeader({ order, status }: Props) {
  return (
    <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
      <Typography variant="h6">Заказ #{order.orderNumber}</Typography>
      <Chip label={getOrderStatusLabel(status)} color={getStatusColor(status)} size="small" />
    </Box>
  );
}
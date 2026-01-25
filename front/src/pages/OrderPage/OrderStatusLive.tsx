import { Box, Chip, Typography } from "@mui/material";
import { getOrderStatusLabel, getStatusColor } from "../../types/orderStatus";
import { formatDateTime } from '../../utils/dateFormatter';
import { OrderStatusChangedEvent } from "../../types/order";

export default function OrderStatusLive(evt: OrderStatusChangedEvent) {
  return (
    <Box display="flex" alignItems="center" gap={1}>
      <Typography variant="body2">Статус заказа изменён:</Typography>
      <Chip label={getOrderStatusLabel(evt.OldStatus)} color={getStatusColor(evt.OldStatus)} size="small" />
      <Typography variant="body2">→</Typography>
      <Chip label={getOrderStatusLabel(evt.NewStatus)} color={getStatusColor(evt.NewStatus)} size="small" />
      <Typography variant="caption" color="textSecondary" ml={1}>{formatDateTime(evt.ChangedAt, true)}</Typography>
    </Box>
  );
}
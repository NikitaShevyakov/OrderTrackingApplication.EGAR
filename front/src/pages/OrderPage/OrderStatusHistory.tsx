import { Box, Typography } from "@mui/material";
import OrderStatusLive from "./OrderStatusLive";
import { OrderStatusChangedEvent } from "../../types/order";

type Props = { events: OrderStatusChangedEvent[] };

export default function OrderStatusHistory({ events }: Props) {
  if (events.length === 0) {
    return <Typography variant="body2" color="textSecondary">Пока нет изменений</Typography>;
  }
  return (
    <Box>
      {events.map((evt, i) => <Box key={i} mt={1}><OrderStatusLive {...evt} /></Box>)}
    </Box>
  );
}
import { Box, Typography } from "@mui/material";
import { formatDateTime } from '../../utils/dateFormatter';

type Props = { createdAt: string; changedAt: string };

export default function OrderMeta({ createdAt, changedAt }: Props) {
  return (
    <Box display="flex" gap={3}>
      <Box>
        <Typography variant="caption" color="textSecondary">Создан:</Typography>
        <Typography variant="body2">{formatDateTime(createdAt)}</Typography>
      </Box>
      <Box>
        <Typography variant="caption" color="textSecondary">Изменен:</Typography>
        <Typography variant="body2">{formatDateTime(changedAt)}</Typography>
      </Box>
    </Box>
  );
}
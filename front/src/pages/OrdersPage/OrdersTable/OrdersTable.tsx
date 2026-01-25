import {
  Paper, Table, TableHead, TableBody, TableFooter, Box,
  TableRow, TableCell, TablePagination,
  IconButton,
  Chip,
} from '@mui/material';
import { tableSx, cellSx } from './OrdersTableStyle';
import { Order } from '../../../types/order';
import { getOrderStatusLabel, getStatusColor } from '../../../types/orderStatus';
import { OrdersFilters } from '../../../Redux/types/ordersTypes';
import TableContainer from '@mui/material/TableContainer';
import { Link as RouterLink } from 'react-router-dom';
import { Edit as EditIcon, DeleteForever as DeleteIcon, Delete  } from '@mui/icons-material';
import { formatDateTime } from '../../../utils/dateFormatter';

interface OrdersTableProps {
  rows: Order[];
  total: number;
  filters: OrdersFilters;
  isLoading?: boolean;
  onPageChange: (page: number) => void;
  onRowsPerPageChange: (limit: number) => void;
  onDeleteOrder: (id: number) => void;
}

const OrdersTable = ({
  rows,
  total,
  filters,
  onPageChange,
  onRowsPerPageChange,
  onDeleteOrder
}: OrdersTableProps) => {

  return (
    <div className="main-page">
      <Paper sx={{ width: '100%', mx: 'auto' }}>
        <Table stickyHeader sx={tableSx}>
          <TableHead>
            <TableRow>
              {[
                { id: "OrderNumber", label: "Номер заказа" },
                { id: "Description", label: "Описание" },
                { id: "Status", label: "Статус" },
                { id: "CreatedAt", label: "Дата создания" },
                { id: "Action", label: "" }
              ].map(column => (
                <TableCell key={column.id} align="center" sx={cellSx} >{column.label}</TableCell>
              ))}
            </TableRow>
          </TableHead>
        </Table>

        <Box sx={{ height: '400px', position: 'relative', overflowY: 'scroll' }}>
          <TableContainer >
            <Table sx={tableSx}>
              <TableBody>
                {rows.map((order: Order) => {
                  return (
                    <TableRow hover key={order.id}>
                      <TableCell align="center" sx={cellSx}>{order.orderNumber}</TableCell>
                      <TableCell align="center" sx={cellSx}>{order.description}</TableCell>
                      <TableCell align="center" sx={cellSx}>
                        <Chip label={getOrderStatusLabel(order.status)} color={getStatusColor(order.status)} size="small" />
                      </TableCell>
                      <TableCell align="center" sx={cellSx}>{formatDateTime(order.createdAt)}</TableCell>
                      <TableCell align="center" sx={cellSx}>
                        <IconButton component={RouterLink} to={`/order/${order.id}`} aria-label="Edit">
                          <EditIcon />
                        </IconButton>
                        <IconButton onClick={() => {onDeleteOrder(order.id)}} aria-label="Delete">
                          <DeleteIcon />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </TableContainer>
        </Box>

        <Table sx={tableSx}>
          <TableFooter>
            <TableRow>
              <TableCell colSpan={3} sx={{ p: 0 }}>
                <TablePagination
                  rowsPerPageOptions={[5, 10, 15, 20]}
                  component="div"
                  count={total}
                  rowsPerPage={filters.limit}
                  page={filters.page - 1}
                  onPageChange={(_, p) => onPageChange(p + 1)}
                  onRowsPerPageChange={e => onRowsPerPageChange(+e.target.value)}
                  labelRowsPerPage="На странице:"
                  labelDisplayedRows={({ from, to, count }) => `${from}–${to} из ${count}`}
                  showFirstButton
                  showLastButton
                />
              </TableCell>
            </TableRow>
          </TableFooter>
        </Table>
      </Paper>
    </div>
  );
};

export default OrdersTable;
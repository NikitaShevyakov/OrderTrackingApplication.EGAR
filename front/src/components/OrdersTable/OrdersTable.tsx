import {
  Paper, Table, TableHead, TableBody, TableFooter, Box,
  TableRow, TableCell, TablePagination,
  TableSortLabel 
} from '@mui/material';
import {tableSx, cellSx} from './OrdersTableStyle';
import { Order, getOrderStatusLabel } from '../../types/order';
import { OrdersFilters } from '../../Redux/types/ordersTypes';
import TableContainer from '@mui/material/TableContainer';

interface OrdersTableProps {
  rows: Order[];
  total: number;
  filters: OrdersFilters;
  onPageChange: (page: number) => void;
  onRowsPerPageChange: (limit: number) => void;
}

const OrdersTable = ({
  rows,
  total,
  filters,
  onPageChange,
  onRowsPerPageChange
}: OrdersTableProps) => {
    
  // данные текущей страницы
  const page = filters.page - 1;
  const rowsPerPage = filters.limit;

  return (
    <div className="main-page">
        <Paper sx={{ width: '100%', mx: 'auto' }}>
          <Table stickyHeader sx={tableSx}>
            <TableHead>
              <TableRow>
                {[
                  { id: "OrderNumber", label: "Номер заказа" },
                  { id: "Description", label: "Описание" },
                  { id: "Status",  label: "Статус" },
                  { id: "CreatedAt",  label: "Дата создания" }
                ].map(column => (
                  <TableCell
                    key={column.id}
                    align="center"
                    sx={cellSx}
                    // sortDirection={filters.sortBy === column.id ? filters.sortOrder : false}
                  >
                    <TableSortLabel
                      // active={filters.sortBy === column.id}
                      // direction={filters.sortBy === column.id ? filters.sortOrder : "asc"}
                      // onClick={() => onSort(column.id)}
                    >
                      {column.label}
                    </TableSortLabel>
                  </TableCell>
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
                        <TableCell align="center" sx={cellSx}>{getOrderStatusLabel(order.status)}</TableCell>
                        <TableCell align="center" sx={cellSx}>{order.createdAt}</TableCell>
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
                    rowsPerPageOptions={[5,10,15,20]}
                    component="div"
                    count={total}
                    rowsPerPage={rowsPerPage}
                    page={page}
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
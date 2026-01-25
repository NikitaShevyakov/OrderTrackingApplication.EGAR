import OrdersTable from "./OrdersTable/OrdersTable";
import CreateOrderModal from "./CreateOrderModal";
import { Add as AddIcon } from '@mui/icons-material';
import { Button, Alert, Box } from '@mui/material';
import useOrdersPage from "./useOrdersPage";

const OrdersPage = () => {
  const {
    rows, total, isLoading,
    openModal, setOpenModal,
    handleCreateNewOrder,
    handleDeleteOrder,
    alertState,
    onPageChange, onRowsPerPageChange,
    filters
  } = useOrdersPage();

  return (
    <div>
      <Box sx={{ display: 'flex', justifyContent: 'flex-end', mb: 1 }} >
        <Button variant="outlined" startIcon={<AddIcon />} onClick={() => setOpenModal(true)}>Создать заказ</Button>
        <CreateOrderModal isOpen={openModal} onClose={() => setOpenModal(false)} onSubmit={handleCreateNewOrder} />
      </Box>

      {alertState.show && <Alert sx={{ mt: 1, mb: 1 }} severity={alertState.severity}>{alertState.text}</Alert>}

      <OrdersTable
        rows={rows}
        total={total}
        filters={filters}
        isLoading={isLoading}
        onPageChange={onPageChange}
        onRowsPerPageChange={onRowsPerPageChange}
        onDeleteOrder={handleDeleteOrder}
      />
    </div>
  );
};

export default OrdersPage;
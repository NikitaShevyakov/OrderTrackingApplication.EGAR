import { Close as CloseIcon } from '@mui/icons-material';
import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  IconButton,
  styled,
  TextField
} from '@mui/material';
import { CreateOrderData } from "../../types/order";
import useFieldValidation from "../../hooks/useFieldValidation";

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));

interface CreateOrderModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: (data: CreateOrderData) => void;
}

export default function CreateOrderModal({ isOpen, onClose, onSubmit }: CreateOrderModalProps) {
  const validateOrderNumber = (value: string) => {
    if (!value.trim()) return 'Номер заказа обязателен';
    if (value.length < 3) return 'Минимум 3 символа';
    if (value.length > 20) return 'Максимум 20 символов';
    if (!/^[A-Za-z0-9-]+$/.test(value)) return 'Только латинские буквы, цифры и дефисы';
    return '';
  };

  const validateDescription = (value: string) => {
    if (!value.trim()) return 'Описание обязательно';
    if (value.length < 10) return 'Минимум 10 символов';
    if (value.length > 50) return 'Максимум 50 символов';
    return '';
  };

  const orderNumber = useFieldValidation('', validateOrderNumber);
  const description = useFieldValidation('', validateDescription);

  const resetForm = () => {
    orderNumber.reset();
    description.reset();
  }

  const handleClose = () => {
    onClose();
    resetForm();
  }

  const handleSubmit = () => {
    const orderNumberError = orderNumber.validate();
    const descriptionError = description.validate();

    if (orderNumberError || descriptionError) {
      return;
    }

    const data: CreateOrderData = {
      orderNumber: orderNumber.value,
      description: description.value
    };
    onSubmit(data);
    resetForm();
    onClose();
  }

  return (
    <BootstrapDialog
      onClose={handleClose}
      aria-labelledby="customized-dialog-title"
      open={isOpen}
      fullWidth
    >
      <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">Новый заказ</DialogTitle>
      <IconButton
        aria-label="close"
        onClick={handleClose}
        sx={(theme) => ({
          position: 'absolute',
          right: 8,
          top: 8,
          color: theme.palette.grey[500],
        })}
      >
        <CloseIcon />
      </IconButton>
      <DialogContent dividers sx={{ py: 3 }}>
        <TextField
          fullWidth
          label="Номер заказа"
          value={orderNumber.value}
          onChange={(e) => orderNumber.handleChange(e.target.value)}
          onBlur={orderNumber.handleBlur}
          placeholder="Введите номер заказа..."
          variant="outlined"
          error={orderNumber.touched && !!orderNumber.error}
          helperText={orderNumber.touched ? orderNumber.error || ' ' : ' '}
        />
        <TextField
          fullWidth
          label="Описание заказа"
          value={description.value}
          onChange={(e) => description.handleChange(e.target.value)}
          onBlur={description.handleBlur}
          placeholder="Введите описание..."
          multiline
          rows={3}
          variant="outlined"
          sx={{ mt: 1 }}
          error={description.touched && !!description.error}
          helperText={description.touched ? description.error || ' ' : ' '}
        />
      </DialogContent>
      <DialogActions>
        <Button autoFocus onClick={handleSubmit}>Создать</Button>
      </DialogActions>
    </BootstrapDialog>
  );
}

import { useCallback } from 'react';
import { CreateOrderData } from '../../types/order';
import useFieldValidation from '../../hooks/useFieldValidation';

interface UseCreateOrderModalProps {
  onClose: () => void;
  onSubmit: (data: CreateOrderData) => void;
}

export default function useCreateOrderModal({ onClose, onSubmit }: UseCreateOrderModalProps) {
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

  const resetForm = useCallback(() => {
    orderNumber.reset();
    description.reset();
  }, [orderNumber, description]);

  const validateForm = useCallback(() => {
    const orderNumberError = orderNumber.validate();
    const descriptionError = description.validate();
    return !orderNumberError && !descriptionError;
  }, [orderNumber, description]);

  const getFormData = useCallback((): CreateOrderData => {
    return {
      orderNumber: orderNumber.value,
      description: description.value
    };
  }, [orderNumber.value, description.value]);

  const handleClose = useCallback(() => {
    onClose();
    resetForm();
  }, [onClose, resetForm]);

  const handleSubmit = useCallback(() => {
    if (!validateForm()) {
      return;
    }

    onSubmit(getFormData());
    resetForm();
    onClose();
  }, [validateForm, onSubmit, getFormData, resetForm, onClose]);

  return {
    orderNumber,
    description,
    handleClose,
    handleSubmit
  };
}

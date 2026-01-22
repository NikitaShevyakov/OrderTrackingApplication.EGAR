import { useState } from "react";

const useFieldValidation = (initialValue = '', validator?: (val: string) => string) => {
  const [value, setValue] = useState(initialValue);
  const [error, setError] = useState('');
  const [touched, setTouched] = useState(false);

  const handleChange = (newValue: string) => {
    setValue(newValue);
    if (touched && validator) {
      setError(validator(newValue));
    }
  };

  const handleBlur = () => {
    setTouched(true);
    if (validator) {
      setError(validator(value));
    }
  };

  const validate = () => {
    setTouched(true);
    if (validator) {
      const validationError = validator(value);
      setError(validationError);
      return validationError;
    }
    return '';
  };

  const reset = () => {
    setValue('');
    setError('');
    setTouched(false);
  };

  return {
    value,
    error,
    touched,
    setValue,
    handleBlur,
    handleChange,
    validate,
    reset
  };
};

export default useFieldValidation;
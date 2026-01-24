import { useState } from 'react';
import {
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
  Box
} from '@mui/material';
import { LabelValueItem } from '../types/labelValueItem';

export interface SimpleSelectProps {
  label: string;
  options: LabelValueItem[];
  value?: string;
  onChange?: (value: string) => void;
  width?: number | string;
  disabled?: boolean;
}

const SimpleSelect = ({
  label,
  options,
  value = '',
  onChange,
  width = 200,
  disabled = false
}: SimpleSelectProps) => {
  const [selectedValue, setSelectedValue] = useState<string>(value);

  const handleChange = (event: SelectChangeEvent<string>) => {
    const newValue = event.target.value;
    setSelectedValue(newValue);
    if (onChange) {
      onChange(newValue);
    }
  };

  const menuItems = options.map((option) => (
    <MenuItem key={option.value} value={option.value}>
      {option.label}
    </MenuItem>
  ));

  return (
    <Box sx={{ width, minWidth: 120 }}>
      <FormControl fullWidth disabled={disabled}>
        <InputLabel id="simple-select-label">{label}</InputLabel>
        <Select
          labelId="simple-select-label"
          id="simple-select"
          value={selectedValue}
          label={label}
          onChange={handleChange}
        >
          {menuItems}
        </Select>
      </FormControl>
    </Box>
  );
};

export default SimpleSelect;
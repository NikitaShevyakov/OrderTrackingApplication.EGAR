import { styled, Theme } from '@mui/material/styles';
import { TableCell } from '@mui/material';

export const tableSx = (theme: Theme) => ({
  tableLayout: 'fixed' as const,
  width: '100%',
  borderBottom: `1px solid ${theme.palette.divider}`,
});

export const cellSx = (theme: Theme) => ({
  fontSize: theme.typography.body2.fontSize,
  lineHeight: theme.typography.body2.lineHeight,
  padding: theme.spacing(1.5),
});

export const PaginationCell = styled(TableCell)(({ theme }: { theme: Theme }) => ({
  padding: 0,
  borderTop: `1px solid ${theme.palette.divider}`,
}));
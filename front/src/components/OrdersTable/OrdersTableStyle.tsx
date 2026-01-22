import { styled, Theme } from '@mui/material/styles';
import { Box, TableRow, TableCell } from '@mui/material';

export const ScrollArea = styled(Box)({
  position: 'relative',
  overflowY: 'auto',
  scrollbarWidth: 'none',
  msOverflowStyle: 'none',
  '&::-webkit-scrollbar': { display: 'none' },
});

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

export const TotalsRow = styled(TableRow)(({ theme }: { theme: Theme }) => ({
  backgroundColor: theme.palette.background.paper,
  '& .MuiTableCell-root': {
    ...cellSx(theme),
    borderTop: `1px solid ${theme.palette.divider}`,
    fontWeight: theme.typography.fontWeightMedium,
  },
}));

export const PaginationCell = styled(TableCell)(({ theme }: { theme: Theme }) => ({
  padding: 0,
  borderTop: `1px solid ${theme.palette.divider}`,
}));
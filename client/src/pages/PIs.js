import { Box, useTheme } from '@mui/material';
import { tokens } from '../theme';
import Header from '../components/Header';

const PerformanceIndicators = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  return (
    <Box display="flex" justifyContent="space-between" alignItems="center">
      <Header title="Performance Indicators" subtitle="PIs" />
    </Box>
  );
};

export default PerformanceIndicators;

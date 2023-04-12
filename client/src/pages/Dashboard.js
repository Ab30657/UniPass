import { Box, useTheme } from '@mui/material';
import { tokens } from '../theme';
import Header from '../components/Header';
import { useContext, useEffect } from 'react';
import LoadingContext from '../context/LoadingContext';

const Dashboard = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  return (
    <Box m="20px">
      {/* HEADER */}
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Header title="UniPass" subtitle="Welcome to your dashboard" />
      </Box>
    </Box>
  );
};

export default Dashboard;

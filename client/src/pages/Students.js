import { Box, useTheme } from '@mui/material';
import { tokens } from '../theme';

const Students = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  return <Box>Hello Students</Box>;
};

export default Students;

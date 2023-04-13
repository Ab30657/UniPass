import { Box, useTheme } from '@mui/material';
import { tokens } from '../../theme';

const Instructors = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  return <Box>Hello Instructors</Box>;
};

export default Instructors;

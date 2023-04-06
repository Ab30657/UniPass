import { Box, useTheme } from '@mui/material';
import { tokens } from '../theme';

const Courses = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  return <Box>Hello Courses</Box>;
};

export default Courses;

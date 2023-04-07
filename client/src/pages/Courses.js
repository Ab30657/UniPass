import { Box, useTheme } from '@mui/material';
import { tokens } from '../theme';
import Header from '../components/Header';

const userList = [
  { id: 1, name: 'Alice', age: 25 },
  { id: 2, name: 'Bob', age: 30 },
  { id: 3, name: 'Charlie', age: 35 },
];
const Courses = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  return (
    <Box m="20px">
      {/* HEADER */}
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Header title="UniPass" subtitle="Here's the list of all Courses" />
      </Box>
    </Box>
  );
};

export default Courses;

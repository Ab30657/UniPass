import { Box, useTheme } from '@mui/material';
import { tokens } from '../theme';
import Header from '../components/Header';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import useAxiosPrivate from '../hooks/useAxiosPrivate';
import React, { useState, useEffect } from 'react';

function createData(id: number, username: string, roles: string) {
  return { id, username, roles };
}

const Students = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  const [rows, setRows] = useState([]);

  const axiosPrivate = useAxiosPrivate();
  useEffect(() => {
    axiosPrivate
      .get('https://localhost:7153/api/Admin/Users')
      .then((response) => {
        // handle successful response
        console.log(response.data);
        const data = response.data.map((user) =>
          createData(user.id, user.username, user.roles),
        );
        setRows(data);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      });
  }, []);

  return (
    <Box m="20px">
      {/* HEADER */}
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Header title="UniPass" subtitle="Here's the list of all Students" />
      </Box>
      <Box m="60px">
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell align="right">Full Name</TableCell>
                <TableCell align="right">Role</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rows.map((row) => (
                <TableRow
                  key={row.id}
                  sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                >
                  <TableCell component="th" scope="row">
                    {row.id}
                  </TableCell>
                  <TableCell align="right">{row.username}</TableCell>
                  <TableCell align="right">{row.roles}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Box>
  );
};

export default Students;

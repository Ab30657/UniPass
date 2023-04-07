import { Box, Typography, useTheme } from '@mui/material';
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
import React, { useState, useEffect, useContext } from 'react';
import axios from '../api/axios';
import LoadingContext from '../context/LoadingContext';
import { DataGrid } from '@mui/x-data-grid';
import {
  AdminPanelSettingsOutlined,
  LockOpenOutlined,
  SecurityOutlined,
} from '@mui/icons-material';

const GET_ALL_USERS_URL = 'admin/users';

// function createData(id: number, username: string, roles: string) {
//   return { id, username, roles };
// }

const Users = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [rows, setRows] = useState([]);
  const { showLoading, hideLoading } = useContext(LoadingContext);
  const axiosPrivate = useAxiosPrivate();
  const columns = [
    { field: 'id', headerName: 'ID' },
    {
      field: 'fullName',
      headerName: 'Full Name',
      flex: 1,
      cellClassName: 'name-column--cell',
    },
    {
      field: 'username',
      headerName: 'Username',
      headerAlign: 'left',
      align: 'left',
    },
    {
      field: 'role',
      headerName: 'Role',
      headerAlign: 'center',
      flex: 1,
      renderCell: ({ row: { roles } }) => {
        let role = roles[0];
        return (
          <Box
            width="60%"
            m="0 auto"
            p="5px"
            display="flex"
            justifyContent="center"
            backgroundColor={
              role === 'Admin'
                ? colors.greenAccent[600]
                : role === 'Student'
                ? colors.greenAccent[700]
                : colors.greenAccent[700]
            }
            borderRadius="4px"
          >
            {role === 'Admin' && <AdminPanelSettingsOutlined />}
            {role === 'Instructor' && <SecurityOutlined />}
            {role === 'Student' && <LockOpenOutlined />}
            <Typography color={colors.grey[100]} sx={{ ml: '5px' }}>
              {role}
            </Typography>
          </Box>
        );
      },
    },
  ];
  useEffect(() => {
    showLoading();

    const response = axiosPrivate
      .get(GET_ALL_USERS_URL)
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        const data = response.data;
        setRows(data);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        // console.log('Hello, World!');
        hideLoading();
      });
  }, []);

  return (
    <Box m="20px">
      <Header title="Users" subtitle="Manage all users" />
      <Box
        m="40px 0 0 0"
        height="75vh"
        sx={{
          '& .MuiDataGrid-root': {
            border: 'none',
          },
          '& .MuiDataGrid-cell': {
            borderBottom: 'none',
          },
          '& .name-column--cell': {
            color: colors.greenAccent[300],
          },
          '& .MuiDataGrid-columnHeaders': {
            backgroundColor: colors.blueAccent[700],
            borderBottom: 'none',
          },
          '& .MuiDataGrid-virtualScroller': {
            backgroundColor: colors.primary[400],
          },
          '& .MuiDataGrid-footerContainer': {
            borderTop: 'none',
            backgroundColor: colors.blueAccent[700],
          },
          '& .MuiCheckbox-root': {
            color: `${colors.greenAccent[200]} !important`,
          },
        }}
      >
        <DataGrid checkboxSelection rows={rows} columns={columns} />
      </Box>
    </Box>
  );
};

export default Users;

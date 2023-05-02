import { Box } from '@mui/material';
import { tokens } from '../../theme';
import Header from '../../components/Header';
import React, { useContext, useEffect, useState } from 'react';
import { Container, Stack } from '@mui/system';
import { Button, SvgIcon } from '@mui/material';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { useTheme } from '@emotion/react';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';
import { useNavigate } from 'react-router-dom';
import { DataGrid } from '@mui/x-data-grid';
import EditPIs from './EditPIs';

const GET_ALL_PI_URL = 'admin/PI';

const PerformanceIndicators = () => {
  const [PIs, setPI] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  useEffect(() => {
    showLoading();

    const response = axiosPrivate
      .get(GET_ALL_PI_URL)
      .then((response) => {
        // handle successful response
        const data = response.data;
        setPI(data);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });
  }, []);

  const columns = [
    { field: 'id', headerName: 'ID', width: 9 },
    { field: 'name', headerName: 'Name', width: 150, editable: true },
    {
      field: 'actions',
      headerName: 'Actions',
      type: 'actions',
      renderCell: (params) => <EditPIs />,
    },
  ];

  const rows = PIs.map((pi) => ({
    id: pi.id,
    name: pi.name,
  }));

  return (
    <>
      <Box m="20px">
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack direction="row" justifyContent="space-between" spacing={4}>
              <Stack spacing={1}>
                <Header
                  title="Performance Indicators"
                  subtitle="Manage all Performance Indicators"
                />
              </Stack>
              <div>
                <Button
                  onClick={() => navigate('Create')}
                  startIcon={
                    <SvgIcon fontSize="small">
                      <PlusIcon />
                    </SvgIcon>
                  }
                  sx={{
                    backgroundColor: colors.blueAccent[700],
                    color: colors.grey[100],
                    fontSize: '14px',
                    fontWeight: 'bold',
                    padding: '10px 20px',
                    boxShadow: 5,
                  }}
                >
                  Add
                </Button>
              </div>
            </Stack>
            <Box sx={{ height: 400, width: '100%' }}>
              <DataGrid
                columns={columns}
                rows={rows}
                initialState={{
                  pagination: {
                    paginationModel: {
                      pageSize: 5,
                    },
                  },
                }}
                pageSizeOptions={[5, 10, 20]}
                checkboxSelection
                disableRowSelectionOnClick
                /*
                getRowSpacing={(params) => ({
                  top: params.isFirstVisible ? 0 : 5,
                  bottom: params.isLastVisible ? 0 : 5,
                })}*/
              />
            </Box>
            <Box
              sx={{
                display: 'flex',
                justifyContent: 'center',
              }}
            ></Box>
          </Stack>
        </Container>
      </Box>
    </>
  );
};

export default PerformanceIndicators;

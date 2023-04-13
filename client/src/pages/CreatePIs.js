import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import InputLabel from '@mui/material/InputLabel';
import { Button, Grid } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useNavigate } from 'react-router-dom';
import LoadingContext from '../context/LoadingContext';
import useAxiosPrivate from '../hooks/useAxiosPrivate';
import Header from '../components/Header';

const POST_NEW_PI_URL = 'admin/PI';

const CreatePIs = () => {
  const [PI, setPI] = useState('');
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  const handleSubmit = async (event) => {
    showLoading();
    event.preventDefault();
    console.log(JSON.stringify(PI));
    const response = axiosPrivate
      .post(POST_NEW_PI_URL, JSON.stringify(PI))
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        navigate('/PIs');
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        // console.log('Hello, World!');
        hideLoading();
      });
  };

  return (
    <Box m="20px">
      <Header
        title="Performance Indicator"
        subtitle="Manage Performance Indicators"
      />
      <Box
        component="form"
        onSubmit={handleSubmit}
        sx={{
          marginTop: 15,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          minWidth: '120',
        }}
        noValidate
        autoComplete="off"
      >
        <Grid container spacing={3}>
          <Grid item xs={12} sm={6}>
            <TextField
              fullWidth
              sx={{ color: colors.grey[700] }}
              id="name"
              label="Performance Indicator"
              variant="filled"
              value={PI}
              onChange={(e) => setPI(e.target.value)}
            />
          </Grid>
        </Grid>

        <Button
          sx={{
            backgroundColor: colors.blueAccent[700],
            color: colors.grey[100],
            fontSize: '14px',
            fontWeight: 'bold',
            padding: '10px 20px',
            boxShadow: 5,
            mt: 3,
            mb: 2,
          }}
          type="submit"
        >
          Create PI
        </Button>
      </Box>
    </Box>
  );
};
export default CreatePIs;

/*

      {success ? (
        <Fab
          color="primary"
          sx={{
            width: 40,
            height: 40,
            bgcolor: green[500],
            '&:hover': { bgcolor: green[700] },
          }}
        >
          <Check />
        </Fab>
      ) : (
        <Fab
          color="primary"
          sx={{
            width: 40,
            height: 40,
          }}
          disabled={params.id !== rowId || loading}
          onClick={handleSubmit}
        >
          <Save />
        </Fab>
      )}
      {loading && (
        <CircularProgress
          size={52}
          sx={{
            color: green[500],
            position: 'absolute',
            top: -6,
            left: -6,
            zIndex: 1,
          }}
        />
      )}
*/

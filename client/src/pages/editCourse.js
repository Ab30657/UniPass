import React, { useState } from 'react';
import { useTheme, Grid, Box, MenuItem, Button } from '@mui/material';
import TextField from '@mui/material/TextField';
import { Container } from '@mui/system';
import { tokens } from '../theme';
import OutlinedInput from '@mui/material/OutlinedInput';
import Header from '../components/Header';
import InputLabel from '@mui/material/InputLabel';
// import { MenuItem } from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import { ThemeProvider } from '@mui/material/styles';
import Select from '@mui/material/Select';
const Editcourse = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [Pi, setPi] = useState([]);
  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(Pi);
  };
  const piName = [
    {
      id: 0,
      name: 'name0',
    },
    {
      id: 1,
      name: 'name1',
    },
    {
      id: 2,
      name: 'name2',
    },
    {
      id: 3,
      name: 'name3',
    },
    {
      id: 4,
      name: 'name4',
    },
    {
      id: 5,
      name: 'name5',
    },
  ];
  return (
    <ThemeProvider theme={theme}>
      <Container maxWidth="lg">
        <Box
          sx={{
            marginTop: 15,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Header title="Edit Course" />
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel
                  id="demo-multiple-name-label"
                  style={{ color: colors.greenAccent[500] }}
                >
                  Pi
                </InputLabel>
                <Select
                  // labelId="demo-multiple-name-label"
                  // id="demo-multiple-name"
                  multiple
                  value={Pi}
                  onChange={(e) => setPi(e.target.value)}
                  input={<OutlinedInput label="Pi" />}
                  // fullWidth
                >
                  {piName.map((description) => {
                    return (
                      <MenuItem
                        key={description.id}
                        value={description.id}
                        style={{ color: colors.grey[100] }}
                      >
                        {description.name}
                      </MenuItem>
                    );
                  })}
                </Select>
              </FormControl>
            </Grid>

            <Button
              style={{
                margin: '0 auto',
                display: 'flex',
                color: colors.greenAccent[500],
              }}
              onClick={handleSubmit}
              type="submit"
              // fullWidth
              variant="contained"
              size="large"
              // sx={ {color: colors.grey[100],}}}
              // fullWidth
              // sx={{ mt: 3, mb: 2 }}
            >
              submit
            </Button>
          </Grid>
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default Editcourse;

// import * as React from 'react';
import React, { useState } from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import { Container } from '@mui/system';
import InputLabel from '@mui/material/InputLabel';
import { Button, Grid } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Select from '@mui/material/Select';
import axios from 'axios';

const Createcourse = () => {
  const [courseTitle, setcourseTitle] = useState('');
  const [selectInstructor, setselectInstructor] = useState('');
  const [selectSemester, setselectSemester] = useState('');
  const theme = createTheme();
  const instructors = [
    {
      id: 0,
      firstName: 'Al;fkd',
      lastName: 'Bsadf',
      appUserId: 0,
    },
    {
      id: 2,
      firstName: 'dlfmkla',
      lastName: 'sadlfmlds;',
      appUserId: 0,
    },
    {
      id: 3,
      firstName: 'dksmfpeqop',
      lastName: 'sdfm;mell3k',
      appUserId: 0,
    },
  ];
  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await axios.post(JSON.stringify({ courseTitle }), {
      headers: { 'Content-Type': 'application/json' },
    });
    // setcourseTitle(e.target.value);
    // setselectInstructor(e.target.value);
    //   const response = await axios.post(
    //     JSON.stringify({
    //       courseTitle,
    //     }),
    //     {
    //       headers: { 'Content-Type': 'application/json' },
    //     },
    //   );
  };
  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <Box
          sx={{
            marginTop: 15,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            // minWidth: '120',
          }}
          onSubmit={handleSubmit}
          noValidate
          autoComplete="off"
        >
          <Grid container spacing={3}>
            <Grid item xs={12} sm={5}>
              <TextField
                id="CourseTitle"
                label="CourseTitle"
                variant="outlined"
                value={courseTitle}
                onChange={(e) => setcourseTitle(e.target.value)}
              />
            </Grid>
            <Grid item xs={12} sm={7}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">
                  Instructor
                </InputLabel>
                <Select
                  // labelId="demo-simple-select-label"
                  // id="demo-simple-select"
                  value={selectInstructor}
                  label="Instructor"
                  // sx={{ minwidth: '120' }}
                  onChange={(e) => setselectInstructor(e.target.value)}
                >
                  {/* <MenuItem value={'sa'}>Sa</MenuItem>
            <MenuItem value={'Ba'}>Ba</MenuItem> */}
                  {instructors.map((instructor) => {
                    return (
                      <MenuItem key={instructor.id} value={instructor.id}>
                        {instructor.firstName} {instructor.lastName}
                      </MenuItem>
                    );
                  })}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12} sm={12}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">Semester</InputLabel>
                <Select
                  // labelId="demo-simple-select-label"
                  // id="demo-simple-select"
                  value={selectSemester}
                  label="Semester"
                  // sx={{ minwidth: '120' }}
                  onChange={(e) => setselectSemester(e.target.value)}
                >
                  <MenuItem value={'Fall2022'}>Fall 2022</MenuItem>
                  <MenuItem value={'Spring2023'}>Spring 2023</MenuItem>
                </Select>
              </FormControl>
            </Grid>
          </Grid>

          <Button
            // onClick={() => console.log('you clicked me')}
            type="submit"
            // fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            submit
          </Button>
        </Box>
      </Container>
    </ThemeProvider>
  );
};
export default Createcourse;

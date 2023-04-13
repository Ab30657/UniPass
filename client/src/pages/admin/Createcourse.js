// import * as React from 'react';
import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import InputLabel from '@mui/material/InputLabel';
import { Button, Grid } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import { useNavigate } from 'react-router-dom';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import Header from '../../components/Header';

const POST_NEW_COURSE_URL = 'admin/courses/create';
const GET_ALL_INSTRUCTORS_URL = 'admin/instructors';

const Createcourse = () => {
  const [courseTitle, setcourseTitle] = useState('');
  const [selectInstructor, setselectInstructor] = useState('');
  const [selectSemester, setselectSemester] = useState('');
  const [instructors, setInstructors] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  useEffect(() => {
    showLoading();
    const response = axiosPrivate
      .get(GET_ALL_INSTRUCTORS_URL)
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        const data = response.data;
        setInstructors(data);
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

  const handleSubmit = async (event) => {
    showLoading();
    event.preventDefault();
    console.log(
      JSON.stringify({
        title: courseTitle,
        instructorId: selectInstructor,
        semesterId: selectSemester,
      }),
    );
    const response = axiosPrivate
      .post(
        POST_NEW_COURSE_URL,
        JSON.stringify({
          title: courseTitle,
          instructorId: selectInstructor,
          semesterId: selectSemester,
        }),
      )
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        navigate('/courses');
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
  const semesters = [
    {
      id: 1,
      name: 'Fall 2023',
    },
  ];

  return (
    <Box m="20px">
      <Header title="Users" subtitle="Manage all users" />
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
              id="CourseTitle"
              label="CourseTitle"
              variant="filled"
              value={courseTitle}
              onChange={(e) => setcourseTitle(e.target.value)}
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <FormControl fullWidth>
              <InputLabel variant="filled" id="demo-simple-select-label">
                Instructor
              </InputLabel>
              <Select
                variant="filled"
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
              <InputLabel variant="filled" id="demo-simple-select-label">
                Semester
              </InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                variant="filled"
                value={selectSemester}
                label="Semester"
                sx={{
                  minwidth: '120',
                  backgroundColor: colors.primary[400],
                }}
                onChange={(e) => setselectSemester(e.target.value)}
              >
                {semesters.map((semester) => {
                  return (
                    <MenuItem value={semester.id}>{semester.name}</MenuItem>
                  );
                })}
              </Select>
            </FormControl>
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
          Create Course
        </Button>
      </Box>
    </Box>
  );
};
export default Createcourse;

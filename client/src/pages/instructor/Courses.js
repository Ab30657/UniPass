// import * as React from 'react';
import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { Container, Stack } from '@mui/system';
import { Button, Grid, SvgIcon, Typography } from '@mui/material';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { CourseCard } from '../../components/CourseCardInstructor';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import Header from '../../components/Header';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';
import { useNavigate } from 'react-router-dom';

const GET_ALL_COURSES_URL = 'instructor/courses';
const Courses = () => {
  const [courses, setCourses] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  useEffect(() => {
    showLoading();

    const response = axiosPrivate
      .get(GET_ALL_COURSES_URL)
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        const data = response.data;
        setCourses(data);
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
    <>
      <Box m="20px">
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack direction="row" justifyContent="space-between" spacing={4}>
              <Stack spacing={1}>
                <Header
                  title="Courses"
                  subtitle="List of courses you are teaching"
                />
              </Stack>
            </Stack>
            <Grid gap={0} container spacing={1}>
              {courses.map((course) => (
                <Grid xs={12} md={6} lg={4} item key={course.id}>
                  <CourseCard course={course} />
                </Grid>
              ))}
            </Grid>
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

export default Courses;

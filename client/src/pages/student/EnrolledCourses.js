// import * as React from 'react';
import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { Container, Stack } from '@mui/system';
import { Grid } from '@mui/material';
import { CourseCard } from '../../components/CourseCardStudent';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import Header from '../../components/Header';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';
import { useNavigate } from 'react-router-dom';

const GET_OWN_COURSES_URL = 'student/courses';

export const EnrolledCourses = () => {
  const [enrolledCourses, setEnrolledCourses] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  useEffect(() => {
    showLoading();
    Promise.all([axiosPrivate.get(GET_OWN_COURSES_URL)])
      .then(([reqAll, reqOwn]) => {
        const data = reqAll.data;
        setEnrolledCourses(data);
      })
      .finally(() => {
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
                <Header title="Your courses" />
              </Stack>
            </Stack>
            <Grid gap={0} container spacing={1}>
              {enrolledCourses.map((course) => (
                <Grid xs={12} md={6} lg={4} item key={course.id}>
                  <CourseCard course={course} hasRegistered={true} />
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

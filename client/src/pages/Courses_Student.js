import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { Container, Stack } from '@mui/system';
import { Button, Grid, SvgIcon, Typography } from '@mui/material';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { CourseCard } from '../components/CourseCard';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import Header from '../components/Header';
import useAxiosPrivate from '../hooks/useAxiosPrivate';
import LoadingContext from '../context/LoadingContext';
import { useNavigate } from 'react-router-dom';

const Courses_Students = () => {
  return (
    <>
      <Box m="20px">
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack direction="row" justifyContent="space-between" spacing={4}>
              <Stack spacing={1}>
                <Header
                  title="Courses for Students"
                  subtitle="Manage all courses"
                />
              </Stack>
            </Stack>
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

export default Courses_Students;

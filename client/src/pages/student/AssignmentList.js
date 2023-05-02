import {
  Box,
  Container,
  Divider,
  Link as MuiLink,
  List,
  Typography,
  ListItemButton,
  ListItemText,
  Stack,
} from '@mui/material';
import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, Link, useParams } from 'react-router-dom';
import Header from '../../components/Header';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';

const courseId = '1';
const GET_STUDENT_ASSIGNMENT_URL = `Student/Courses/`;
const GET__ALL_COURSES_URL = 'Student/Courses';
const GET_COURSE_URL = 'Student/Courses/';

const style = {
  width: '100%',
  maxWidth: 900,
  bgcolor: 'background.paper',
};

const useStyle = {
  '&:hover': {
    cursor: 'pointer',
    color: '#ffffff !important',
    boxShadow: 'none !important',
  },
  '&:active': {
    boxShadow: 'none !important',
    color: '#3c52b2 !important',
  },
};

const AssignmentList = () => {
  const [Assignment, setAssignment] = useState([]);
  const [course, setCourse] = useState({ instructors: [] });
  let { courseId } = useParams();
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  useEffect(() => {
    showLoading();

    const res = axiosPrivate
      .get(GET_COURSE_URL + courseId)
      .then((res) => {
        setCourse(res.data);
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });

    const response = axiosPrivate
      .get(GET_STUDENT_ASSIGNMENT_URL + courseId + '/Materials')
      .then((response) => {
        const data = response.data;
        setAssignment(data);
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });
  }, []);

  return (
    <Box m="20px">
      <Container maxWidth="xl">
        <Stack spacing={3}>
          <Stack direction="row" justifyContent="space-between" spacing={4}>
            <Stack spacing={1}>
              <Header
                title={course?.title}
                subtitle={`Instructor: ${
                  course?.instructors[0]?.firstName +
                  ' ' +
                  course?.instructors[0]?.lastName
                }`}
              />
            </Stack>
          </Stack>
          <Box
            gridColumn="span 4"
            gridRow="span 2"
            backgroundColor={colors.primary[400]}
            overflow="auto"
          >
            <Box
              display="flex"
              justifyContent="space-between"
              alignItems="center"
              borderBottom={`4px solid ${colors.primary[500]}`}
              colors={colors.grey[100]}
              p="15px"
            >
              <Typography
                color={colors.grey[100]}
                variant="h5"
                fontWeight="600"
              >
                Assignments
              </Typography>
            </Box>
            {Assignment?.map((material, i) => (
              <Box
                key={`${material.txId}-${i}`}
                display="flex"
                justifyContent="space-between"
                alignItems="center"
                borderBottom={`4px solid ${colors.primary[500]}`}
                p="15px"
              >
                <Box onClick={() => navigate(`${material.id}`)}>
                  <Typography
                    color={colors.greenAccent[500]}
                    variant="h5"
                    fontWeight="600"
                    sx={useStyle}
                  >
                    {material.title}
                  </Typography>
                </Box>
                <Box color={colors.grey[100]}>{material.date}</Box>
                <Box
                  backgroundColor={colors.greenAccent[500]}
                  p="5px 10px"
                  borderRadius="4px"
                  minWidth={100}
                >
                  Full Points: {material.fullMarks}
                </Box>
              </Box>
            ))}
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
  );
};

export default AssignmentList;

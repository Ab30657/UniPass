import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { Container, Stack, bgcolor } from '@mui/system';
import { Button, CardActions, Grid, SvgIcon, Typography } from '@mui/material';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { useNavigate, useParams } from 'react-router-dom';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import Header from '../../components/Header';
import { ClassOutlined } from '@mui/icons-material';
import { Avatar, Card, CardContent, Divider } from '@mui/material';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';

const GET_ALL_ASSIGNMENTS_URL = 'instructor/courses/';
const GET_ALL_STUDENTS = 'instructor/courses/';

const colorStyle = (vl) => {
  if (vl < 18) return '#ff0d0d';
  if (vl < 36) return '#ff4e11';
  if (vl < 52) return '#ff8e15';
  if (vl < 70) return '#fab733';
  if (vl < 88) return '#acb334';
  if (vl < 100) return '#69b34c';
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

const ListAssignment = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [assignments, setAssignments] = useState([]);
  const [students, setStudents] = useState([]);
  let { courseId } = useParams();
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  useEffect(() => {
    showLoading();

    Promise.all([
      axiosPrivate.get(GET_ALL_ASSIGNMENTS_URL + `${courseId}/Materials`),
      axiosPrivate.get(GET_ALL_STUDENTS + `${courseId}/students?semesterId=1`),
    ])
      .then(([response, studentResponse]) => {
        const data = response.data;
        setAssignments(data);
        setStudents(studentResponse.data);
      })
      .finally(() => {
        hideLoading();
      });
  }, []);

  return (
    <Box m="20px">
      <Container maxWidth="xl">
        <Stack spacing={1}>
          <Stack direction="row" justifyContent="space-between" spacing={4}>
            <Stack spacing={1}>
              <Header title="View Assignments" />
            </Stack>
          </Stack>
          <Box
            display="flex"
            justifyContent="space-between"
            alignItems="center"
            borderBottom={`4px solid ${colors.primary[500]}`}
            bgcolor={colors.blueAccent[600]}
            colors={colors.grey[100]}
            p="15px"
          >
            <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
              Course Materials
            </Typography>
          </Box>
          <Grid gap={1} container spacing={0}>
            {assignments.map((assignments) => (
              <Grid xs={12} md={6} lg={3} item key={assignments.id}>
                <Card
                  sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    height: '100%',
                    backgroundColor: colors.primary[400],
                  }}
                >
                  <CardContent>
                    <Typography
                      textAlign="center"
                      gutterBottom
                      variant="h5"
                      component="div"
                    >
                      <ClassOutlined
                        sx={{
                          color: colors.greenAccent[600],
                          fontSize: '25px',
                        }}
                      />
                    </Typography>
                    <Typography
                      align="center"
                      color={colors.greenAccent[500]}
                      gutterBottom
                      variant="h5"
                    >
                      {assignments.title}
                    </Typography>
                  </CardContent>
                  <CardActions disableSpacing sx={{ mt: 'auto' }}>
                    <Button
                      onClick={() => navigate(`${assignments.id}`)}
                      sx={{
                        backgroundColor: colors.greenAccent[700],
                        color: colors.grey[100],
                        fontSize: '12px',
                        fontWeight: 'bold',
                        padding: '5px 10px',
                        boxShadow: 5,
                        '&:hover': { bgcolor: colors.greenAccent[800] },
                      }}
                      size="small"
                    >
                      View Assignment
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
          <Button
            onClick={() => navigate('New')}
            startIcon={
              <SvgIcon fontSize="small">
                <PlusIcon />
              </SvgIcon>
            }
            sx={{
              backgroundColor: colors.redAccent[600],
              color: colors.grey[100],
              fontSize: '14px',
              fontWeight: 'bold',
              padding: '10px 20px',
              boxShadow: 5,
              '&:hover': {
                bgcolor: colors.redAccent[700],
              },
            }}
          >
            New Assignment
          </Button>
          <Box maxWidth="xl">
            <Box
              gridColumn="span 4"
              gridRow="span 2"
              backgroundColor={colors.primary[400]}
              overflow="auto"
              mt="20px"
            >
              <Box
                display="flex"
                justifyContent="space-between"
                alignItems="center"
                borderBottom={`4px solid ${colors.primary[500]}`}
                bgcolor={colors.blueAccent[600]}
                colors={colors.grey[100]}
                p="15px"
              >
                <Typography
                  color={colors.grey[100]}
                  variant="h5"
                  fontWeight="600"
                >
                  Enrolled students
                </Typography>
              </Box>
              {students?.map((student, i) => (
                <Box
                  key={`${student.txId}-${i}`}
                  display="flex"
                  justifyContent="space-between"
                  alignItems="center"
                  borderBottom={`4px solid ${colors.primary[500]}`}
                  p="15px"
                >
                  <Box
                    onClick={() =>
                      navigate(`/Courses/${courseId}/${student.id}/Grades`)
                    }
                  >
                    <Typography
                      color={colors.greenAccent[500]}
                      variant="h5"
                      fontWeight="600"
                      sx={useStyle}
                    >
                      {student.firstName + ' ' + student.lastName}
                    </Typography>
                  </Box>
                  <Box color={colors.grey[100]}></Box>
                  <Box
                    backgroundColor={colorStyle(student.grade)}
                    p="5px 10px"
                    borderRadius="4px"
                    minWidth={100}
                    fontWeight="700"
                  >
                    {/* Full Points: {material.fullMarks} */}
                    Grade: {student.grade}
                  </Box>
                </Box>
              ))}
            </Box>
          </Box>
        </Stack>
      </Container>
    </Box>
  );
};
export default ListAssignment;

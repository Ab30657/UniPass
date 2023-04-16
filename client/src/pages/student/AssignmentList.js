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
import { useNavigate, Link } from 'react-router-dom';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import Divider from '@mui/material/Divider';
import { AssistWalker } from '@mui/icons-material';
import Grid from '@mui/material/Grid';

const GET_STUDENT_ASSIGNMENT_URL = 'Student/Courses/1/Materials';
const GET__ALL_COURSES_URL = 'Student/Courses';
const style = {
  width: '100%',
  maxWidth: 900,
  bgcolor: 'background.paper',
};

const AssignmentList = () => {
  const [Assignment, setAssignment] = useState([]);
  const [Courses, setCourses] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  useEffect(() => {
    showLoading();

    const res = axiosPrivate
      .get(GET__ALL_COURSES_URL)
      .then((res) => {
        const data = res.data;
        setCourses(data);
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });

    const response = axiosPrivate
      .get(GET_STUDENT_ASSIGNMENT_URL)
      .then((response) => {
        // handle successful response
        //console.log(response.data);
        const data = response.data;
        setAssignment(data);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });
  }, []);
  //console.log(Courses);

  const rows = Assignment.map((material) => ({
    id: material.id,
    title: material.title,
    semesterId: material.semesterId,
    courseId: material.courseId,
  }));

  return (
    <>
      <Box m="20px">
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack direction="row" justifyContent="space-between" spacing={4}>
              <Stack spacing={1}>
                <Header
                  title="Assignment"
                  subtitle={`Course ID: ${Assignment.courseId}`}
                />
              </Stack>
            </Stack>
            <Box sx={{ height: 400, width: '100%' }}>
              <List sx={style} component="nav" aria-label="mailbox folders">
                {Assignment.map((material) => (
                  <div key={material.id}>
                    <Link to={`/Courses/Assignment/Quiz/${material.id}`}>
                      <ListItem button>
                        <ListItemText primary={material.title} />
                      </ListItem>
                    </Link>
                    <Divider />
                  </div>
                ))}
              </List>
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

export default AssignmentList;

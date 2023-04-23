import {
  Box,
  Container,
  Divider,
  Link as MuiLink,
  List,
  ListItemButton,
  ListItemText,
  Stack,
} from '@mui/material';
import React, { useContext, useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import Header from '../../components/Header';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';

const GET_STUDENT_COURSE_URL = 'Student/Courses/';
const style = {
  width: '100%',
  maxWidth: 900,
  bgcolor: 'background.paper',
};

const AssignmentList = () => {
  const [Assignment, setAssignment] = useState([]);
  const [course, setCourses] = useState(null);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  const { courseId } = useParams();

  useEffect(() => {
    showLoading();

    axiosPrivate
      .get(GET_STUDENT_COURSE_URL + courseId)
      .then((res) => {
        const data = res.data;
        console.log(data);
        setCourses(data);
      })
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });

    axiosPrivate
      .get(GET_STUDENT_COURSE_URL + courseId + `/Materials`)
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
    <>
      <Box m="20px">
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack direction="row" justifyContent="space-between" spacing={4}>
              <Stack spacing={1}>
                <Header
                  title="Assignment"
                  subtitle={
                    course ? `Course Title: ${course.title}` : 'Loading...'
                  }
                />
              </Stack>
            </Stack>
            <Box sx={{ height: 400, width: '100%' }}>
              <List sx={style} component="nav" aria-label="mailbox folders">
                {Assignment.map((material) => (
                  <div key={material.id}>
                    <MuiLink
                      underline="none"
                      color="inherit"
                      to={`/Courses/${material.courseId}/Materials/${material.id}`}
                      component={Link}
                    >
                      <ListItemButton>
                        <ListItemText primary={material.title} />
                      </ListItemButton>
                    </MuiLink>

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

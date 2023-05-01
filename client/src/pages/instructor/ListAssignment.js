import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { Container, Stack } from '@mui/system';
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

const ListAssignment = () => {
  //   const [assignment, setAssignment] = useState('');
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [assignments, setAssignments] = useState([]);
  let { courseId } = useParams();
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  useEffect(() => {
    showLoading();

    const response = axiosPrivate
      .get(GET_ALL_ASSIGNMENTS_URL + `${courseId}/Materials`)
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        const data = response.data;
        setAssignments(data);
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
    <Box m="20px">
      <Container maxWidth="xl">
        <Stack spacing={3}>
          <Stack direction="row" justifyContent="space-between" spacing={4}>
            <Stack spacing={1}>
              <Header title="View Assignments" />
            </Stack>
            <div>
              <Button
                onClick={() => navigate('New')}
                startIcon={
                  <SvgIcon fontSize="small">
                    <PlusIcon />
                  </SvgIcon>
                }
                sx={{
                  backgroundColor: colors.blueAccent[700],
                  color: colors.grey[100],
                  fontSize: '14px',
                  fontWeight: 'bold',
                  padding: '10px 20px',
                  boxShadow: 5,
                }}
              >
                Add
              </Button>
            </div>
          </Stack>
          <Grid gap={0} container spacing={1}>
            {assignments.map((assignments) => (
              <Grid xs={12} md={6} lg={4} item key={assignments.id}>
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
                      variant="h4"
                    >
                      {assignments.title}
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <Button
                      onClick={() => navigate(`${assignments.id}`)}
                      sx={{
                        backgroundColor: colors.greenAccent[700],
                        color: colors.grey[100],
                        fontSize: '14px',
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
        </Stack>
      </Container>
    </Box>
  );
};
export default ListAssignment;

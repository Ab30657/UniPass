import React, { useContext, useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import { Container, Stack } from '@mui/system';
import { Button, Grid, SvgIcon, Typography } from '@mui/material';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { useNavigate } from 'react-router-dom';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import Header from '../components/Header';
import { ClassOutlined } from '@mui/icons-material';
import { Avatar, Card, CardContent, Divider } from '@mui/material';

const ListAssignment = () => {
  const [assignment, setAssignment] = useState('');
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const assignmentlist = [
    { id: 0, title: 'Assignment0' },
    { id: 1, title: 'Assignment1' },
    { id: 2, title: 'Assignment2' },
  ];
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
            {assignmentlist.map((assignments) => (
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
                    <Box
                      sx={{
                        display: 'flex',
                        justifyContent: 'center',
                        pb: 3,
                      }}
                    >
                      <ClassOutlined
                        sx={{
                          color: colors.greenAccent[600],
                          fontSize: '25px',
                        }}
                      />
                    </Box>
                    <Typography
                      align="center"
                      color={colors.greenAccent[500]}
                      gutterBottom
                      variant="h3"
                    >
                      {assignments.title}
                    </Typography>
                  </CardContent>
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

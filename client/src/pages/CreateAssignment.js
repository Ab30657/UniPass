import React, { useContext, useEffect, useState } from 'react';
import { Container, Stack } from '@mui/system';
import Box from '@mui/material/Box';
import Header from '../components/Header';
import TextField from '@mui/material/TextField';
import { Button, Grid, MenuItem, FormControl, Radio } from '@mui/material';
import { useTheme } from '@emotion/react';
import IconButton from '@mui/material/IconButton';
import RemoveIcon from '@mui/icons-material/Remove';
import InputLabel from '@mui/material/InputLabel';
import AddIcon from '@mui/icons-material/Add';
import Select from '@mui/material/Select';
import OutlinedInput from '@mui/material/OutlinedInput';
import { tokens } from '../theme';
import useAxiosPrivate from '../hooks/useAxiosPrivate';
import LoadingContext from '../context/LoadingContext';
import { useParams } from 'react-router-dom';

const GET_ALL_PIs = 'instructor/courses/';

const CreateAssignment = () => {
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  let { courseId } = useParams();
  useEffect(() => {
    showLoading();
    const response = axiosPrivate
      .get(GET_ALL_PIs + `${courseId}/PI`)
      .then((response) => {
        const data = response.data;
        setPi(data);
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
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState();
  const [question, setQuestion] = useState([
    { Description: '', Option1: '', Option2: '', Option3: '', Option4: '' },
  ]);
  const [Pi, setPi] = useState([]);
  const handleChange = (index, event) => {
    const values = [...question];
    values[index][event.target.name] = event.target.value;
    setQuestion(values);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Question', question);
  };
  const handleAdd = () => {
    setQuestion([
      ...question,
      { Description: '', Option1: '', Option2: '', Option3: '', Option4: '' },
    ]);
  };

  const handleRemove = (index) => {
    const values = [...question];
    values.splice(index, 1);
    setQuestion(values);
  };

  return (
    <Box
      component="form"
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        minWidth: '120',
      }}
      noValidate
      autoComplete="off"
    >
      <Container maxWidth="md">
        <Header title="Create Assignment" />

        <Grid container spacing={3}>
          <Grid item xs={10} sm={10}>
            <TextField
              fullWidth
              sx={{ color: colors.grey[700] }}
              id="CourseTitle"
              label="Course Title"
              variant="filled"
              value={title}
            />
          </Grid>
          <Grid item xs={10} sm={10}>
            {question.map((questions, index) => (
              <div key={index}>
                <Stack spacing={3}>
                  <TextField
                    name="Question"
                    label="Question"
                    variant="filled"
                    value={questions.Description}
                    fullWidth
                    onChange={(event) => handleChange(index, event)}
                  ></TextField>
                  <Grid item xs={6} sm={6}>
                    <Stack spacing={3}>
                      {[1, 2, 3, 4].map((val, index) => {
                        return (
                          <>
                            <Grid columns={2}>
                              <TextField
                                name={`Option${val}`}
                                label={`Option${val}`}
                                variant="filled"
                                value={questions.Option1}
                                onChange={(event) => handleChange(index, event)}
                              ></TextField>
                              <Radio
                                // onChange={handleChange}
                                name={`radio-buttons-${questions.id}`}
                                inputProps={{ 'aria-label': 'A' }}
                              />
                            </Grid>
                          </>
                        );
                      })}
                    </Stack>
                  </Grid>
                  <FormControl fullWidth>
                    <InputLabel
                      variant="filled"
                      id="demo-multiple-name-label"
                      style={{ color: colors.greenAccent[500] }}
                    >
                      Performance Indicators
                    </InputLabel>
                    <Select
                      variant="filled"
                      multiple
                      value={Pi}
                      onChange={(e) => setPi(e.target.value)}
                      fullWidth
                      input={<OutlinedInput label="Pi" />}
                      MenuProps={{
                        sx: {
                          '&& .Mui-selected': {
                            backgroundColor: colors.greenAccent[600],
                          },
                        },
                      }}
                    >
                      {Pi.map((description) => {
                        return (
                          <MenuItem
                            key={description.id}
                            value={description.id}
                            style={{ color: colors.grey[100] }}
                          >
                            {description.name}
                          </MenuItem>
                        );
                      })}
                    </Select>
                    <Stack direction="row" spacing={5}>
                      <IconButton onClick={() => handleRemove(index)}>
                        <RemoveIcon />
                      </IconButton>
                      <IconButton onClick={() => handleAdd()}>
                        <AddIcon />
                      </IconButton>
                    </Stack>
                  </FormControl>
                </Stack>
              </div>
            ))}
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
              fullWidth
              type="submit"
              onClick={handleSubmit}
            >
              Create Assignment
            </Button>
          </Grid>
        </Grid>
      </Container>
    </Box>
  );
};
export default CreateAssignment;

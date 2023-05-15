import React, { useContext, useEffect, useState } from 'react';
import { Container, Stack } from '@mui/system';
import Box from '@mui/material/Box';
import Header from '../../components/Header';
import TextField from '@mui/material/TextField';
import { Button, Grid, MenuItem, FormControl } from '@mui/material';
import { useTheme } from '@emotion/react';
import IconButton from '@mui/material/IconButton';
import InputLabel from '@mui/material/InputLabel';
import AddIcon from '@mui/icons-material/Add';
import Select from '@mui/material/Select';
import { tokens } from '../../theme';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';
import { useNavigate, useParams } from 'react-router-dom';

const GET_ALL_PIs = 'instructor/courses/';
const POST_CREATE_ASSIGNMENT = 'instructor/courses/';

const CreateAssignment = () => {
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);
  let { courseId } = useParams();
  const theme = useTheme();
  const navigate = useNavigate();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  const [Pi, setPi] = useState([]);

  const handleSubmitAssignment = () => {
    const response = axiosPrivate
      .post(
        POST_CREATE_ASSIGNMENT + courseId + `/Materials`,
        JSON.stringify({ title, questions, semesterId: 1 }),
      )
      .then((response) => {
        navigate(`/Courses/${courseId}/Materials`);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });
  };
  const [questions, setQuestions] = useState([
    {
      questionText: '',
      fullMarks: 0,
      performanceIndicators: [],
      answers: [{ answerText: '', correct: true }],
    },
  ]);

  const handleAddQuestion = () => {
    setQuestions([
      ...questions,
      {
        questionText: '',
        fullMarks: 0,
        performanceIndicators: [],
        answers: [{ answerText: '', correct: true }],
      },
    ]);
  };

  const handleChangeQuestion = (e, index) => {
    const { name, value } = e.target;
    const updatedQuestions = [...questions];
    updatedQuestions[index][name] = value;
    setQuestions(updatedQuestions);
  };

  const handleChangeAnswer = (e, questionIndex, answerIndex) => {
    const { name, value } = e.target;
    const updatedQuestions = [...questions];
    updatedQuestions[questionIndex].answers[answerIndex][name] = value;
    setQuestions(updatedQuestions);
  };

  const handleAddAnswer = (questionIndex) => {
    const updatedQuestions = [...questions];
    updatedQuestions[questionIndex].answers.push({
      answerText: '',
      correct: false,
    });
    setQuestions(updatedQuestions);
  };
  const handleSelectCorrectAnswer = (e, questionIndex) => {
    const { value } = e.target;
    const updatedQuestions = [...questions];
    updatedQuestions[questionIndex].answers.forEach((answer, index) => {
      // Set all other answers in the question as incorrect
      if (index !== parseInt(value)) {
        answer.correct = false;
      } else {
        // Set the selected answer as correct
        answer.correct = true;
      }
    });
    setQuestions(updatedQuestions);
  };
  const handleChangePerformanceIndicators = (e, questionIndex) => {
    const { value } = e.target;
    const updatedQuestions = [...questions];
    updatedQuestions[questionIndex].performanceIndicators = value;
    setQuestions(updatedQuestions);
  };
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
        hideLoading();
      });
  }, []);

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
              label="Assignment Title"
              variant="filled"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
            />
          </Grid>
          <Grid item xs={10} sm={10}>
            <h3>Questions</h3>
            {questions.map((question, index) => (
              <div key={index}>
                <Stack spacing={3}>
                  <Grid container spacing={2}>
                    <Grid item xs={8}>
                      <TextField
                        name="questionText"
                        label="Question"
                        variant="filled"
                        value={question.Description}
                        fullWidth
                        onChange={(event) => handleChangeQuestion(event, index)}
                      ></TextField>
                    </Grid>
                    <Grid item xs={4}>
                      <TextField
                        name="fullMarks"
                        label="FullMarks"
                        variant="filled"
                        value={question.fullMarks}
                        fullWidth
                        onChange={(event) => handleChangeQuestion(event, index)}
                      ></TextField>
                    </Grid>
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
                      value={question.performanceIndicators}
                      onChange={(e) =>
                        handleChangePerformanceIndicators(e, index)
                      }
                      fullWidth
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
                  </FormControl>
                  <Grid item xs={6} sm={6}>
                    <FormControl>
                      <Stack spacing={3}>
                        {question.answers.map((answer, answerIndex) => (
                          <Grid key={answerIndex} columns={2}>
                            <TextField
                              name={`answerText`}
                              label={`Option ${answerIndex + 1}`}
                              variant="filled"
                              value={answer.answerText}
                              onChange={(event) =>
                                handleChangeAnswer(event, index, answerIndex)
                              }
                            ></TextField>
                          </Grid>
                        ))}
                      </Stack>
                      <Stack
                        direction="row"
                        justifyContent="center"
                        spacing={5}
                        alignItems="center"
                      >
                        <IconButton onClick={() => handleAddAnswer(index)}>
                          <AddIcon />
                        </IconButton>
                      </Stack>
                    </FormControl>
                    <FormControl fullWidth variant="outlined">
                      <InputLabel
                        variant="filled"
                        id="demo-multiple-name-label"
                        style={{ color: colors.greenAccent[500] }}
                        htmlFor={`correct-answer-${index}`}
                      >
                        Correct Answer
                      </InputLabel>
                      <Select
                        variant="filled"
                        fullWidth
                        MenuProps={{
                          sx: {
                            '&& .Mui-selected': {
                              backgroundColor: colors.greenAccent[600],
                            },
                          },
                        }}
                        value={
                          question.answers.findIndex((x) => x.correct) == '-1'
                            ? 0
                            : question.answers.findIndex((x) => x.correct)
                        }
                        onChange={(e) => handleSelectCorrectAnswer(e, index)}
                        label="Correct Answer"
                        inputProps={{
                          id: `correct-answer-${index}`,
                        }}
                      >
                        {question.answers.map((val, ind) => (
                          <MenuItem key={ind} value={ind.toString()}>
                            {val.answerText}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  </Grid>
                  <Stack
                    direction="row"
                    justifyContent="center"
                    spacing={5}
                    alignItems="center"
                  >
                    <IconButton onClick={() => handleAddQuestion()}>
                      <AddIcon />
                    </IconButton>
                  </Stack>
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
              type="button"
              onClick={handleSubmitAssignment}
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

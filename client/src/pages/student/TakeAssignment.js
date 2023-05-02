import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, Link, useParams } from 'react-router-dom';
import {
  Card,
  CardContent,
  Typography,
  FormControl,
  RadioGroup,
  FormControlLabel,
  Radio,
  Button,
} from '@mui/material';
import { Box } from '@mui/system';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import Header from '../../components/Header';

const POST_SUBMIT_ASSIGNMENT = 'Student/Courses/';

const TakeAssignment = () => {
  const [assignment, setAssignment] = useState({});
  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [userAnswers, setUserAnswers] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  const [questions, setQuestions] = useState([]);
  const [takeAssignments, setTakeAssignments] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  const { courseId, assignmentId } = useParams();

  useEffect(() => {
    const fetchData = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(
          `Student/Courses/${courseId}/Materials/${assignmentId}`,
        );
        //testing
        if (response.data.takeAssignments.length > 0)
          navigate(`Grade`, { replace: true });
        // console.log(response.data);
        setAssignment(response.data);
        setTitle(response.data.title);
        setQuestions(response.data.questions);
        setTakeAssignments(response.data.questions);
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, []);

  const handleAnswerChange = (questionId, event) => {
    const newAnswers = userAnswers;
    if (newAnswers.find((x) => x.questionId === questionId)) {
      newAnswers.find((x) => x.questionId === questionId).answerId = parseInt(
        event.target.value,
      );
    } else {
      newAnswers.push({
        questionId: questionId,
        answerId: parseInt(event.target.value),
      });
    }
    // user[currentQuestion].answerId = parseInt(event.target.value);
    // console.log(newAnswers);
    setUserAnswers(newAnswers);
  };

  const handleNextQuestion = () => {
    setCurrentQuestion(currentQuestion + 1);
  };

  const submitAnswer = () => {
    // console.log(userAnswers);
    const response = axiosPrivate
      .post(
        POST_SUBMIT_ASSIGNMENT + courseId + `/Materials/${assignmentId}`,
        JSON.stringify({ takeQuestions: userAnswers }),
      )
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        navigate(`/Courses/${courseId}/Materials/${assignmentId}/Grade`);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        // console.log('Hello, World!');
        hideLoading();
      });
  };
  return (
    <Box m="20px">
      <Header title={title} />
      <Box
        gridColumn="span 4"
        gridRow="span 2"
        backgroundColor={colors.primary[400]}
        overflow="auto"
      >
        {questions?.map((question, index) => (
          <div key={index}>
            <Box
              key={index}
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
                {index + 1}. {question.questionText}
              </Typography>
            </Box>
            <Box
              key={`${question.txId}-${index}`}
              display="flex"
              justifyContent="space-between"
              alignItems="center"
              borderBottom={`4px solid ${colors.primary[500]}`}
              p="15px"
            >
              <Box>
                <Typography
                  color={colors.greenAccent[500]}
                  variant="h5"
                  fontWeight="600"
                >
                  <FormControl component="fieldset">
                    <RadioGroup
                      onChange={(e) => handleAnswerChange(question.id, e)}
                    >
                      {question?.answers.map((option, optionIndex) => (
                        <FormControlLabel
                          key={optionIndex}
                          value={option.id}
                          control={<Radio />}
                          label={option.answerText}
                        />
                      ))}
                    </RadioGroup>
                    {currentQuestion === index && (
                      <Button
                        variant="contained"
                        disabled={userAnswers[index] === null}
                        onClick={handleNextQuestion}
                      >
                        Next
                      </Button>
                    )}
                  </FormControl>
                </Typography>
              </Box>
              <Box
                backgroundColor={colors.greenAccent[500]}
                p="5px 10px"
                borderRadius="4px"
                minWidth={100}
              >
                Points: {question.fullMarks}
              </Box>
            </Box>
          </div>
        ))}
      </Box>
      {currentQuestion === questions.length && (
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
          onClick={submitAnswer}
        >
          Submit Assignment
        </Button>
      )}
    </Box>
  );
};

export default TakeAssignment;

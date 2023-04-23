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

const POST_SUBMIT_ASSIGNMENT = 'student/courses/';

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
      {questions.map((question, index) => (
        <div key={index} style={{ marginBottom: '20px' }}>
          <Card>
            <CardContent>
              <Typography variant="h5" gutterBottom>
                Question {index + 1} of {questions.length}
              </Typography>
              <Typography variant="body1" gutterBottom>
                {question.questionText}
              </Typography>
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
            </CardContent>
          </Card>
        </div>
      ))}
      {currentQuestion === questions.length && (
        <Box mt="20px">
          <Button variant="contained" onClick={submitAnswer}>
            Submit Assignment
          </Button>
        </Box>
      )}
    </Box>
  );
};

export default TakeAssignment;

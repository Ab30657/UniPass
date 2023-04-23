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

const TakeAssignment = () => {
  const [assignment, setAssignment] = useState(null);
  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [userAnswers, setUserAnswers] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
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
        setAssignment(response.data);
        setUserAnswers(new Array(response.data.questions.length).fill(null));
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, [axiosPrivate, courseId, assignmentId, showLoading, hideLoading]);

  const handleAnswerChange = (event) => {
    const newAnswers = [...userAnswers];
    newAnswers[currentQuestion] = parseInt(event.target.value);
    setUserAnswers(newAnswers);
  };

  const handleNextQuestion = () => {
    setCurrentQuestion(currentQuestion + 1);
  };

  const calculateScore = () => {
    let score = 0;
    for (let i = 0; i < quizData.length; i++) {
      if (
        userAnswers[i] ===
        quizData[i].options.findIndex((option) => option.isCorrect)
      ) {
        score++;
      }
    }
    return score;
  };

  if (!assignment) {
    return null; // render loading spinner or message
  }

  const { title, questions, takeAssignments } = assignment;
  const quizData = questions.map((question) => {
    const options = question.performanceIndicators.map((option) => ({
      answer: option.name,
      isCorrect: false, // replace with correct answer logic
    }));
    return {
      question: question.questionText,
      options,
    };
  });

  const score = calculateScore();

  return (
    <Box m="20px">
      {quizData.map((question, index) => (
        <div key={index} style={{ marginBottom: '20px' }}>
          <Card>
            <CardContent>
              <Typography variant="h5" gutterBottom>
                Question {index + 1} of {quizData.length}
              </Typography>
              <Typography variant="body1" gutterBottom>
                {question.question}
              </Typography>
              <FormControl component="fieldset">
                <RadioGroup
                  value={userAnswers[index]}
                  onChange={handleAnswerChange}
                >
                  {question.options.map((option, optionIndex) => (
                    <FormControlLabel
                      key={optionIndex}
                      value={optionIndex}
                      control={<Radio />}
                      label={option.answer}
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
      {currentQuestion === quizData.length && (
        <Box mt="20px">
          <Card>
            <CardContent>
              <Typography variant="h5" gutterBottom>
                Quiz Completed!
              </Typography>
              <Typography variant="body1" gutterBottom>
                You scored {score} out of {quizData.length} (
                {(score / quizData.length) * 100}%).
              </Typography>
            </CardContent>
          </Card>
        </Box>
      )}
    </Box>
  );
};

export default TakeAssignment;

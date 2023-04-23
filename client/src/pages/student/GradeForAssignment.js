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

const GradeForAssignment = () => {
  const [assignment, setAssignment] = useState({});
  const [userAnswers, setUserAnswers] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  const [questions, setQuestions] = useState([]);
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
        console.log(response.data);
        setAssignment(response.data);
        setTitle(response.data.title);
        if (response.data.questions) {
          setQuestions(response.data.questions);
        }
        if (response.data.takeAssignment) {
          const updatedUserAnswers = response.data.questions.map((question) => {
            const userAnswer = response.data.takeAssignment.takeQuestions.find(
              (answer) => answer.questionId === question.id,
            );
            if (userAnswer) {
              return userAnswer;
            } else {
              return {
                questionId: question.id,
                answerText: '',
                correct: false,
              };
            }
          });
          setUserAnswers(updatedUserAnswers);
        }
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, []);

  const calculateScore = () => {
    let score = 0;
    userAnswers.forEach((answer, index) => {
      if (answer && answer.correct) {
        score += questions[index].fullMarks;
      }
    });
    return score;
  };

  const final_score = calculateScore();

  return (
    <Box m="20px">
      <Card>
        <CardContent>
          <Typography variant="h5" gutterBottom>
            {title}
          </Typography>
          {questions &&
            questions.map((question, index) => (
              <div key={index} style={{ marginBottom: '20px' }}>
                <Typography variant="body1" gutterBottom>
                  {question.questionText}
                </Typography>
                <FormControl component="fieldset">
                  <RadioGroup value={userAnswers[index]?.answerText}>
                    {question.performanceIndicators.map(
                      (option, optionIndex) => (
                        <FormControlLabel
                          key={optionIndex}
                          value={option.name}
                          control={<Radio disabled />}
                          label={option.name}
                        />
                      ),
                    )}
                  </RadioGroup>
                  {userAnswers[index]?.correct ? (
                    <Typography variant="caption" color="green">
                      Correct
                    </Typography>
                  ) : (
                    <Typography variant="caption" color="red">
                      Incorrect. The correct answer is:{' '}
                      {userAnswers[index]?.correctAnswer}
                    </Typography>
                  )}
                </FormControl>
              </div>
            ))}
          <Typography variant="h6" gutterBottom>
            Score: {final_score} out of{' '}
            {questions.reduce((acc, question) => acc + question.fullMarks, 0)}
          </Typography>
        </CardContent>
      </Card>
    </Box>
  );
};

export default GradeForAssignment;

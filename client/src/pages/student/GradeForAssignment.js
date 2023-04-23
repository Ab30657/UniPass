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
        //console.log(response.data.takeQuestions.length);
        setAssignment(response.data);
        setTitle(response.data.title);
        if (response.data.questions) {
          setQuestions(response.data.questions);
        }
        if (response.data.takeAssignment) {
          setUserAnswers(response.data.takeAssignment.takeQuestions);
        }
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, []);
  console.log(userAnswers);
  const calculateScore = () => {
    let score = 0;
    for (let i = 0; i < userAnswers.length; i++) {
      if (userAnswers[i].correct) {
        console.log(userAnswers[i]);
        score += 1;
      }
    }
    //console.log(score);
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
                  {userAnswers[index]?.correct && (
                    <Typography variant="caption" color="green">
                      Correct
                    </Typography>
                  )}
                  {!userAnswers[index]?.correct && (
                    <Typography variant="caption" color="red">
                      Incorrect
                    </Typography>
                  )}
                </FormControl>
              </div>
            ))}
          <Typography variant="h6" gutterBottom>
            Total Score: {final_score}/ {userAnswers.length}
          </Typography>
        </CardContent>
      </Card>
    </Box>
  );
};

export default GradeForAssignment;

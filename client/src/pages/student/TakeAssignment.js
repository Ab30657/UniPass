import React, { useState } from 'react';
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

const TakeAssignment = () => {
  const [quizData] = useState([
    {
      question: 'What is the capital of France?',
      options: [
        { answer: 'Paris', isCorrect: true },
        { answer: 'London', isCorrect: false },
        { answer: 'Berlin', isCorrect: false },
      ],
    },
    {
      question: 'What is the largest country in the world?',
      options: [
        { answer: 'USA', isCorrect: false },
        { answer: 'China', isCorrect: false },
        { answer: 'Russia', isCorrect: true },
      ],
    },
  ]);

  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [userAnswers, setUserAnswers] = useState(
    new Array(quizData.length).fill(null),
  );

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

  const score = calculateScore();

  return (
    <Box m="20px">
      <Card>
        <CardContent>
          {currentQuestion < quizData.length ? (
            <>
              <Typography variant="h5" gutterBottom>
                Question {currentQuestion + 1} of {quizData.length}
              </Typography>
              <Typography variant="body1" gutterBottom>
                {quizData[currentQuestion].question}
              </Typography>
              <FormControl component="fieldset">
                <RadioGroup
                  value={userAnswers[currentQuestion]}
                  onChange={handleAnswerChange}
                >
                  {quizData[currentQuestion].options.map((option, index) => (
                    <FormControlLabel
                      key={index}
                      value={index}
                      control={<Radio />}
                      label={option.answer}
                    />
                  ))}
                </RadioGroup>
              </FormControl>
              <Button
                variant="contained"
                disabled={userAnswers[currentQuestion] === null}
                onClick={handleNextQuestion}
              >
                Next
              </Button>
            </>
          ) : (
            <>
              <Typography variant="h5" gutterBottom>
                Quiz Complete!
              </Typography>
              <Typography variant="body1" gutterBottom>
                You scored {score} out of {quizData.length} (
                {(score / quizData.length) * 100}%).
              </Typography>
            </>
          )}
        </CardContent>
      </Card>
    </Box>
  );
};

export default TakeAssignment;

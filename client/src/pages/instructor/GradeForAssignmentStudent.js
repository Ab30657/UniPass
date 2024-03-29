import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, Link, useParams } from 'react-router-dom';
import { ResponsiveRadar } from '@nivo/radar';
import {
  Card,
  CardContent,
  Typography,
  FormControl,
  RadioGroup,
  FormControlLabel,
  Radio,
  Button,
  Chip,
  getAccordionDetailsUtilityClass,
} from '@mui/material';
import { Box } from '@mui/system';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import Header from '../../components/Header';
const GradeForAssignmentStudent = () => {
  const [assignmentTake, setAssignmentTake] = useState({});
  const [userAnswers, setUserAnswers] = useState([]);
  const [questions, setQuestions] = useState([]);
  const navigate = useNavigate();
  const { studentId } = useParams();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  //   const [questions, setQuestions] = useState([]);
  const [piScores, setPiScores] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  const { courseId, assignmentId } = useParams();

  useEffect(() => {
    const fetchData1 = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(
          `Instructor/Courses/${courseId}/Materials/${assignmentId}/${studentId}`,
        );
        //testing
        // if (response.data.takeAssignments.length > 0) navigate(`Grade`);
        setTitle(response.data.title);
        setQuestions(response.data.questions);
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData1();

    const fetchData = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(
          `Instructor/Courses/${courseId}/Materials/${assignmentId}/${studentId}/grades`,
        );
        //testing
        setAssignmentTake(response.data);
        setTitle(response.data.title);
        // if (response.data.questions) {
        //   setQuestions(response.data.questions);
        // }
        setPiScores(
          response.data.performanceIndicatorScores.map((el) => ({
            name: el.name,
            Score: (el.score / el.fullMarks) * 100,
          })),
        );
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

  //   const calculateScore = () => {
  //     let score = 0;
  //     userAnswers.forEach((answer, index) => {
  //       if (answer && answer.correct) {
  //         score += questions[index].fullMarks;
  //       }
  //     });
  //     return score;
  //   };

  //   const final_score = calculateScore();

  return (
    <Box m="20px">
      <Header
        title={
          title +
          (assignmentTake.student &&
            ': ' +
              assignmentTake?.student.firstName +
              ' ' +
              assignmentTake?.student.lastName)
        }
        subtitle={`Your Score: ${assignmentTake?.grade}/${assignmentTake?.fullMarks}`}
      />
      <Box
        gridColumn="span 4"
        gridRow="span 2"
        backgroundColor={colors.primary[400]}
        overflow="auto"
      >
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          borderBottom={`4px solid ${colors.primary[500]}`}
          backgroundColor={colors.blueAccent[500]}
          p="15px"
        >
          <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
            Answers
          </Typography>
        </Box>
        {assignmentTake?.takeQuestions?.map((question, index) => (
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
                {index + 1}.{' '}
                {questions &&
                  questions.length > 0 &&
                  questions[index].questionText}
              </Typography>
              <Typography
                color={colors.grey[100]}
                variant="h6"
                fontWeight="600"
              >
                {questions &&
                  questions.length > 0 &&
                  questions[index]?.performanceIndicators.map((pi, piIndex) => (
                    <Chip key={piIndex} color="success" label={pi.name} />
                  ))}
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
                    <RadioGroup value={question?.answerText}>
                      {questions &&
                        questions.length > 0 &&
                        questions[index]?.answers.map((option, optionIndex) => (
                          <FormControlLabel
                            key={optionIndex}
                            value={option.answerText}
                            control={<Radio disabled />}
                            label={option.answerText}
                          />
                        ))}
                    </RadioGroup>
                  </FormControl>
                </Typography>
                <Typography color={colors.grey[100]}>
                  Points:{' '}
                  {question.correct
                    ? `${
                        questions &&
                        questions.length > 0 &&
                        questions[index].fullMarks
                      }`
                    : '0'}
                  /
                  {questions &&
                    questions.length > 0 &&
                    questions[index].fullMarks}
                </Typography>
              </Box>
              <Box
                backgroundColor={
                  question?.correct
                    ? colors.greenAccent[500]
                    : colors.redAccent[500]
                }
                p="5px 10px"
                borderRadius="4px"
                minWidth={100}
              >
                {question?.correct ? 'Correct Answer: ' : 'Incorrect Answer: '}
                {question?.correctAnswer}
              </Box>
            </Box>
          </div>
        ))}
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          borderBottom={`4px solid ${colors.primary[500]}`}
          backgroundColor={colors.blueAccent[500]}
          p="15px"
        >
          <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
            Statistics For Your Attempt
          </Typography>
        </Box>
        <Box
          display="flex"
          flexDirection="column"
          justifyContent="center"
          alignItems="center"
          borderBottom={`4px solid ${colors.primary[500]}`}
          p="15px"
          height="75vh"
        >
          {/*Insert charts here  */}
          <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
            Performance Indicator Scores
          </Typography>
          {assignmentTake && (
            <ResponsiveRadar
              theme={{
                axis: {
                  domain: {
                    line: {
                      stroke: colors.grey[100],
                    },
                  },
                  legend: {
                    text: {
                      fill: colors.grey[100],
                    },
                  },
                  ticks: {
                    line: {
                      stroke: colors.grey[100],
                      strokeWidth: 1,
                    },
                    text: {
                      fill: colors.grey[100],
                    },
                  },
                },
                legends: {
                  text: {
                    fill: colors.grey[100],
                  },
                },
                tooltip: {
                  container: {
                    background: colors.blueAccent[400],
                  },
                },
              }}
              data={piScores}
              keys={['Score']}
              indexBy="name"
              valueFormat=">-.2f"
              margin={{ top: 70, right: 80, bottom: 40, left: 80 }}
              borderColor={{ from: 'color' }}
              gridLabelOffset={36}
              dotColor={{ theme: 'background' }}
              dotBorderWidth={2}
              dotLabel="key"
              colors={{ scheme: 'accent' }}
              blendMode="multiply"
              motionConfig="wobbly"
              maxValue="100"
              legends={[
                {
                  anchor: 'top-left',
                  direction: 'column',
                  translateX: -50,
                  translateY: -40,
                  itemWidth: 80,
                  itemHeight: 20,
                  itemTextColor: '#999',
                  symbolSize: 12,
                  symbolShape: 'circle',
                  effects: [
                    {
                      on: 'hover',
                      style: {
                        itemTextColor: '#000',
                      },
                    },
                  ],
                },
              ]}
            />
          )}
        </Box>
      </Box>
    </Box>
  );
};

export default GradeForAssignmentStudent;

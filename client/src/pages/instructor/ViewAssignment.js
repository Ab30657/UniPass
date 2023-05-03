import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Typography, Button, SvgIcon, Slider } from '@mui/material';
import { Box, Stack } from '@mui/system';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import Header from '../../components/Header';
import ClockIcon from '@heroicons/react/24/solid/ClockIcon';
import { ResponsiveRadar } from '@nivo/radar';
import { ResponsiveBar } from '@nivo/bar';

function valuetext(value) {
  return `${value}`;
}

const POST_SUBMIT_ASSIGNMENT = 'Student/Courses/';
const colorStyle = (vl) => {
  if (vl < 18) return '#ff0d0d';
  if (vl < 36) return '#ff4e11';
  if (vl < 52) return '#ff8e15';
  if (vl < 70) return '#fab733';
  if (vl < 88) return '#acb334';
  if (vl <= 100) return '#69b34c';
};

const useStyle = {
  '&:hover': {
    cursor: 'pointer',
    color: '#ffffff !important',
    boxShadow: 'none !important',
  },
  '&:active': {
    boxShadow: 'none !important',
    color: '#3c52b2 !important',
  },
};

const ViewAssignment = () => {
  //   const [assignment, setAssignment] = useState({});
  const [currentQuestion, setCurrentQuestion] = useState(0);
  const [gScores, setgScores] = useState({});
  const [userAnswers, setUserAnswers] = useState([]);
  const [inputScores, setInputScores] = useState([]);
  const [gapValue, setGapValue] = useState(10);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  const [questions, setQuestions] = useState([]);
  const [piScores, setPiScores] = useState([]);
  const [takeAssignments, setTakeAssignments] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  const { courseId, assignmentId } = useParams();

  useEffect(() => {
    if (takeAssignments.length > 0) {
      var scores = takeAssignments.map((attempt, i) => attempt.grade);
      scores.sort((a, b) => a - b);

      const maxScore = takeAssignments[0].fullMarks;
      const rangeCount = Math.ceil(maxScore / gapValue);
      const ranges = Array.from(Array(rangeCount), (_, i) => ({
        start: i * gapValue,
        end: i * gapValue + gapValue,
      }));

      const cumulativeFrequency = {};
      ranges.forEach((range) => {
        cumulativeFrequency[`${range.start}-${range.end}`] = 0;
      });
      scores.forEach((score) => {
        ranges.forEach((range) => {
          if (score >= range.start && score <= range.end) {
            cumulativeFrequency[`${range.start}-${range.end}`] += 1;
          }
        });
      });
      setgScores(cumulativeFrequency);
    }
  }, [gapValue, takeAssignments]);
  useEffect(() => {
    const fetchData = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(
          `Instructor/Assignment/${assignmentId}/grades`,
        );
        //testing
        // if (response.data.takeAssignments.length > 0) navigate(`Grade`);
        // setAssignment(response.data);
        setTitle(response.data[0].title);
        // setQuestions(response.data.questions);
        setTakeAssignments(response.data);
        var piData = {};
        for (var attempt in response.data) {
          for (var i in response.data[attempt].performanceIndicatorScores) {
            var piSc = response.data[attempt].performanceIndicatorScores[i];
            piData[piSc.name] = piData[piSc.name]
              ? piData[piSc.name]
              : { score: 0, fullMarks: piSc.fullMarks };
            piData[piSc.name].score += piSc.score;
          }
        }
        for (var i in piData) {
          piData[i].score =
            (piData[i].score / (response.data.length * piData[i].fullMarks)) *
            100;
        }
        setPiScores(
          Object.keys(piData).map((el) => ({
            name: el,
            Score: piData[el].score,
          })),
        );
        var scores = response.data.map((attempt, i) => attempt.grade);
        scores.sort((a, b) => a - b);

        const rangeSize = 10;
        const maxScore = scores[scores.length - 1];
        const rangeCount = Math.ceil(maxScore / rangeSize);
        const ranges = Array.from(Array(rangeCount), (_, i) => ({
          start: i * rangeSize,
          end: i * rangeSize + rangeSize,
        }));

        const cumulativeFrequency = {};
        ranges.forEach((range) => {
          cumulativeFrequency[`${range.start}-${range.end}`] = 0;
        });
        scores.forEach((score) => {
          ranges.forEach((range) => {
            if (score >= range.start && score <= range.end) {
              cumulativeFrequency[`${range.start}-${range.end}`] += 1;
            }
          });
        });
        setgScores(cumulativeFrequency);
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, []);

  useEffect(() => {
    var data = [];
    for (var k in gScores) {
      var sub = {};
      sub['range'] = k;
      sub['Scored'] = gScores[k];
      data.push(sub);
    }
    setInputScores(data);
  }, [gScores]);

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
    setUserAnswers(newAnswers);
  };

  const handleNextQuestion = () => {
    setCurrentQuestion(currentQuestion + 1);
  };

  const submitAnswer = () => {
    const response = axiosPrivate
      .post(
        POST_SUBMIT_ASSIGNMENT + courseId + `/Materials/${assignmentId}`,
        JSON.stringify({ takeQuestions: userAnswers }),
      )
      .then((response) => {
        // handle successful response
        navigate(`/Courses/${courseId}/Materials/${assignmentId}/Grade`);
      })
      .catch((error) => {
        // handle error
        console.error(error);
      })
      .finally(() => {
        hideLoading();
      });
  };
  return (
    <Box m="20px">
      <Stack direction="row" justifyContent="space-between" spacing={4}>
        <Stack spacing={1}>
          <Header title={title} />
        </Stack>
        <div>
          <Button
            startIcon={
              <SvgIcon fontSize="small">
                <ClockIcon />
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
            View Statistics
          </Button>
        </div>
      </Stack>
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
          colors={colors.grey[100]}
          bgcolor={colors.blueAccent[500]}
          p="15px"
        >
          <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
            Student Attempts
          </Typography>
          <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
            {/* Full Marks: {takeAssignments ? takeAssignments[0].fullMarks : '0'} */}
          </Typography>
        </Box>
        {takeAssignments?.map((material, i) => (
          <Box
            key={`${material.txId}-${i}`}
            display="flex"
            justifyContent="space-between"
            alignItems="center"
            borderBottom={`4px solid ${colors.primary[500]}`}
            p="15px"
          >
            <Box onClick={() => navigate(`${material.student.id}/grades`)}>
              <Typography
                color={colors.greenAccent[500]}
                variant="h5"
                fontWeight="600"
                sx={useStyle}
              >
                {material.student.firstName + ' ' + material.student.lastName}
              </Typography>
            </Box>
            <Box color={colors.grey[100]}>{material.date}</Box>
            <Box
              backgroundColor={colorStyle(
                (material.grade / material.fullMarks) * 100,
              )}
              p="5px 10px"
              borderRadius="4px"
              minWidth={100}
            >
              Points: {material.grade}
            </Box>
          </Box>
        ))}
      </Box>
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
        }}
      ></Box>

      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        borderBottom={`4px solid ${colors.primary[500]}`}
        backgroundColor={colors.blueAccent[500]}
        p="15px"
      >
        <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
          Statistics For The Assignment
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
        bgcolor={colors.primary[400]}
      >
        {/*Insert charts here  */}
        <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
          Average Performance Indicator Scores for the Assignment
        </Typography>
        {/* <Typography color={colors.grey[100]} variant="h5" fontWeight="600"> */}
        {/* </Typography> */}
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
      </Box>
      <Box
        display="flex"
        flexDirection="column"
        justifyContent="center"
        alignItems="center"
        borderBottom={`4px solid ${colors.primary[500]}`}
        p="50px"
        height="75vh"
        bgcolor={colors.primary[400]}
      >
        {/*Insert charts here  */}
        <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
          Student Score Distribution
        </Typography>
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          width="100%"
          p="15px"
          bgcolor={colors.primary[400]}
        >
          <Slider
            aria-label="Temperature"
            defaultValue={10}
            getAriaValueText={valuetext}
            valueLabelDisplay="auto"
            step={5}
            onChange={(e) => setGapValue(e.target.value)}
            marks
            min={5}
            max={
              takeAssignments && takeAssignments.length > 0
                ? takeAssignments[0].fullMarks
                : 100
            }
            sx={{
              backgroundColor: colors.blueAccent[700],
              color: colors.grey[100],
              fontSize: '14px',
              fontWeight: 'bold',
              boxShadow: 5,
            }}
          />
        </Box>
        {inputScores && (
          <ResponsiveBar
            data={inputScores}
            theme={{
              // added
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
            keys={['Scored']}
            indexBy="range"
            margin={{ top: 50, right: 130, bottom: 50, left: 60 }}
            padding={0.3}
            valueScale={{ type: 'linear' }}
            indexScale={{ type: 'band', round: true }}
            colors={{ scheme: 'accent' }}
            defs={[
              {
                id: 'dots',
                type: 'patternDots',
                background: 'inherit',
                color: '#38bcb2',
                size: 4,
                padding: 1,
                stagger: true,
              },
              {
                id: 'lines',
                type: 'patternLines',
                background: 'inherit',
                color: '#eed312',
                rotation: -45,
                lineWidth: 6,
                spacing: 10,
              },
            ]}
            borderColor={{
              from: 'color',
              modifiers: [['darker', '1.6']],
            }}
            axisTop={null}
            axisRight={null}
            axisBottom={{
              tickSize: 5,
              tickPadding: 5,
              tickRotation: 0,
              legendPosition: 'middle',
              legendOffset: 32,
            }}
            axisLeft={{
              tickSize: 5,
              tickPadding: 5,
              tickRotation: 0,
              legendPosition: 'middle',
              legendOffset: -40,
            }}
            enableLabel={false}
            labelSkipWidth={12}
            labelSkipHeight={12}
            labelTextColor={{
              from: 'color',
              modifiers: [['darker', 1.6]],
            }}
            role="application"
            // barAriaLabel={function (e) {
            //   return (
            //     e.id + ': ' + e.formattedValue + ' in country: ' + e.indexValue
            //   );
            // }}
          />
        )}
      </Box>
    </Box>
  );
};

export default ViewAssignment;

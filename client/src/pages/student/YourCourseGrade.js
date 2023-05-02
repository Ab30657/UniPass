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
  SvgIcon,
  Slider,
  Chip,
  Switch,
} from '@mui/material';
import { Box, Stack } from '@mui/system';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import Header from '../../components/Header';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import ClockIcon from '@heroicons/react/24/solid/ClockIcon';
import { ResponsiveRadar } from '@nivo/radar';
import { ResponsiveBar } from '@nivo/bar';
import { setIn } from 'formik';
import { ResponsiveLine } from '@nivo/line';

const POST_SUBMIT_ASSIGNMENT = 'Student/Courses/';

const colorStyle = (vl) => {
  if (vl < 18) return '#ff0d0d';
  if (vl < 36) return '#ff4e11';
  if (vl < 52) return '#ff8e15';
  if (vl < 70) return '#fab733';
  if (vl < 88) return '#acb334';
  if (vl <= 100) return '#69b34c';
};
const data = [
  {
    id: 'japan',
    color: 'hsl(17, 70%, 50%)',
    data: [
      {
        x: 'plane',
        y: 102,
      },
      {
        x: 'helicopter',
        y: 243,
      },
      {
        x: 'boat',
        y: 135,
      },
      {
        x: 'train',
        y: 29,
      },
      {
        x: 'subway',
        y: 61,
      },
      {
        x: 'bus',
        y: 110,
      },
      {
        x: 'car',
        y: 40,
      },
      {
        x: 'moto',
        y: 58,
      },
      {
        x: 'bicycle',
        y: 255,
      },
      {
        x: 'horse',
        y: 104,
      },
      {
        x: 'skateboard',
        y: 99,
      },
      {
        x: 'others',
        y: 189,
      },
    ],
  },
  {
    id: 'france',
    color: 'hsl(317, 70%, 50%)',
    data: [
      {
        x: 'plane',
        y: 64,
      },
      {
        x: 'helicopter',
        y: 115,
      },
      {
        x: 'boat',
        y: 179,
      },
      {
        x: 'train',
        y: 165,
      },
      {
        x: 'subway',
        y: 29,
      },
      {
        x: 'bus',
        y: 104,
      },
      {
        x: 'car',
        y: 81,
      },
      {
        x: 'moto',
        y: 245,
      },
      {
        x: 'bicycle',
        y: 228,
      },
      {
        x: 'horse',
        y: 221,
      },
      {
        x: 'skateboard',
        y: 61,
      },
      {
        x: 'others',
        y: 169,
      },
    ],
  },
  {
    id: 'us',
    color: 'hsl(56, 70%, 50%)',
    data: [
      {
        x: 'plane',
        y: 164,
      },
      {
        x: 'helicopter',
        y: 17,
      },
      {
        x: 'boat',
        y: 111,
      },
      {
        x: 'train',
        y: 124,
      },
      {
        x: 'subway',
        y: 217,
      },
      {
        x: 'bus',
        y: 163,
      },
      {
        x: 'car',
        y: 124,
      },
      {
        x: 'moto',
        y: 264,
      },
      {
        x: 'bicycle',
        y: 100,
      },
      {
        x: 'horse',
        y: 170,
      },
      {
        x: 'skateboard',
        y: 269,
      },
      {
        x: 'others',
        y: 79,
      },
    ],
  },
  {
    id: 'germany',
    color: 'hsl(163, 70%, 50%)',
    data: [
      {
        x: 'plane',
        y: 124,
      },
      {
        x: 'helicopter',
        y: 275,
      },
      {
        x: 'boat',
        y: 147,
      },
      {
        x: 'train',
        y: 203,
      },
      {
        x: 'subway',
        y: 209,
      },
      {
        x: 'bus',
        y: 289,
      },
      {
        x: 'car',
        y: 62,
      },
      {
        x: 'moto',
        y: 94,
      },
      {
        x: 'bicycle',
        y: 7,
      },
      {
        x: 'horse',
        y: 26,
      },
      {
        x: 'skateboard',
        y: 202,
      },
      {
        x: 'others',
        y: 11,
      },
    ],
  },
  {
    id: 'norway',
    color: 'hsl(151, 70%, 50%)',
    data: [
      {
        x: 'plane',
        y: 120,
      },
      {
        x: 'helicopter',
        y: 47,
      },
      {
        x: 'boat',
        y: 212,
      },
      {
        x: 'train',
        y: 92,
      },
      {
        x: 'subway',
        y: 44,
      },
      {
        x: 'bus',
        y: 166,
      },
      {
        x: 'car',
        y: 289,
      },
      {
        x: 'moto',
        y: 170,
      },
      {
        x: 'bicycle',
        y: 213,
      },
      {
        x: 'horse',
        y: 265,
      },
      {
        x: 'skateboard',
        y: 51,
      },
      {
        x: 'others',
        y: 161,
      },
    ],
  },
];
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

const YourCourseGrade = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [lineData, setLineData] = useState([]);
  const [isStacked, setIsStacked] = useState(true);
  const [piScores, setPiScores] = useState([]);
  const [takeAssignments, setTakeAssignments] = useState([]);
  const [student, setStudent] = useState({});

  const [grade, setGrade] = useState({
    grade: 0,
    fullMarks: 0,
  });

  const axiosPrivate = useAxiosPrivate();

  const { showLoading, hideLoading } = useContext(LoadingContext);

  const { courseId, studentId } = useParams();

  useEffect(() => {
    const fetchData = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(
          `Student/Courses/${courseId}/StudentReports?semesterId=1`,
        );
        setTakeAssignments(response.data.assignmentGradesDtos);
        setStudent(response.data.student);
        setGrade({
          grade: response.data.grade,
          fullMarks: response.data.fullMarks,
        });
        let testData = [
          {
            id: 'overall',
            data: response.data.assignmentGradesDtos.map((assignment, i) => ({
              x: assignment.title,
              y: assignment.grade,
            })),
          },
        ];
        testData = testData.concat(
          response.data.performanceIndicatorScores.map((piScore, j) => ({
            id: piScore.name,
            data: response.data.assignmentGradesDtos.map((asgn, k) => {
              if (
                asgn.performanceIndicatorScores.find((x) => x.id === piScore.id)
              ) {
                return {
                  x: asgn.title,
                  y:
                    (asgn.performanceIndicatorScores.find(
                      (x) => x.id === piScore.id,
                    ).score *
                      100) /
                    asgn.performanceIndicatorScores.find(
                      (x) => x.id === piScore.id,
                    ).fullMarks,
                  z: asgn.performanceIndicatorScores.find(
                    (x) => x.id === piScore.id,
                  ).fullMarks,
                  za: asgn.performanceIndicatorScores.find(
                    (x) => x.id === piScore.id,
                  ).score,
                };
              } else {
                return {
                  x: asgn.title,
                  y: 0,
                };
              }
            }),
          })),
        );
        setLineData(testData);

        var piData = {};
        for (var i in response.data.performanceIndicatorScores) {
          var piSc = response.data.performanceIndicatorScores[i];
          piData[piSc.name] = piData[piSc.name]
            ? piData[piSc.name]
            : { score: 0, fullMarks: piSc.fullMarks };
          piData[piSc.name].score += piSc.score;
        }
        for (var i in piData) {
          piData[i].score = (piData[i].score / piData[i].fullMarks) * 100;
        }

        setPiScores(
          Object.keys(piData).map((el) => ({
            name: el,
            Score: piData[el].score,
          })),
        );
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, []);

  return (
    <Box m="20px">
      <Stack direction="row" justifyContent="space-between" spacing={4}>
        <Stack spacing={1}>
          <Header
            title={student && student?.firstName + ' ' + student?.lastName}
            subtitle={`Your Score: ${grade?.grade}/${grade?.fullMarks}`}
          />
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
            Assignment Scores
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
            <Box
              onClick={() =>
                navigate(`/Courses/${courseId}/Materials/${material.id}`, {
                  replace: true,
                })
              }
            >
              <Typography
                color={colors.greenAccent[500]}
                variant="h5"
                fontWeight="600"
                sx={useStyle}
              >
                {material.title}
              </Typography>
            </Box>
            <Box color={colors.grey[100]}>
              <Typography
                color={colors.grey[100]}
                variant="h6"
                fontWeight="600"
              >
                {takeAssignments[i]?.performanceIndicatorScores.map(
                  (pi, piIndex) => (
                    <Chip
                      sx={{ margin: '1px' }}
                      key={piIndex}
                      color="success"
                      label={pi.name + ': ' + pi.score + '/' + pi.fullMarks}
                    />
                  ),
                )}
              </Typography>
            </Box>
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
          Statistics For The Course
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
          Performance Indicator Scores
        </Typography>
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
        justifyContent="space-between"
        alignItems="center"
        borderBottom={`4px solid ${colors.primary[500]}`}
        p="15px"
        bgcolor={colors.blueAccent[500]}
      >
        {/*Insert charts here  */}
        <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
          Student Assignment Score Trend
        </Typography>
        <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
          <FormControlLabel
            control={<Switch defaultChecked />}
            label="Stacked"
            value={isStacked}
            onChange={(e) => setIsStacked(!isStacked)}
          />
        </Typography>
      </Box>
      <Box
        display="flex"
        flexDirection="column"
        justifyContent="center"
        alignItems="center"
        width="100%"
        p="15px"
        height="75vh"
        bgcolor={colors.primary[400]}
      >
        <ResponsiveLine
          data={lineData}
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
          margin={{ top: 50, right: 210, bottom: 50, left: 160 }}
          xScale={{ type: 'point' }}
          yScale={{
            type: 'linear',
            min: isStacked ? 'auto' : '0',
            max: isStacked ? 'auto' : '100',
            stacked: isStacked,
            reverse: false,
          }}
          yFormat=" >-.2f"
          axisTop={null}
          axisRight={null}
          axisBottom={{
            tickSize: 5,
            tickPadding: 5,
            tickRotation: 0,
            legend: 'Assignments',
            legendOffset: 36,
            legendPosition: 'middle',
          }}
          axisLeft={{
            tickSize: 5,
            tickPadding: 5,
            tickRotation: 0,
            legend: 'Score',
            legendOffset: -40,
            legendPosition: 'middle',
          }}
          pointSize={10}
          colors={{ scheme: 'accent' }}
          pointColor={{ theme: 'background' }}
          pointBorderWidth={2}
          pointBorderColor={{ from: 'serieColor' }}
          pointLabelYOffset={-12}
          useMesh={true}
          legends={[
            {
              anchor: 'bottom-right',
              direction: 'column',
              justify: false,
              translateX: 100,
              translateY: 0,
              itemsSpacing: 0,
              itemDirection: 'left-to-right',
              itemWidth: 80,
              itemHeight: 20,
              itemOpacity: 0.75,
              symbolSize: 12,
              symbolShape: 'circle',
              symbolBorderColor: 'rgba(0, 0, 0, .5)',
              effects: [
                {
                  on: 'hover',
                  style: {
                    itemBackground: 'rgba(0, 0, 0, .03)',
                    itemOpacity: 1,
                  },
                },
              ],
            },
          ]}
        />
      </Box>
    </Box>
  );
};

export default YourCourseGrade;

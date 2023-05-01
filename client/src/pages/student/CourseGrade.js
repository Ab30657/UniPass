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

function valuetext(value) {
  return `${value}`;
}

// const data = [
//   {
//     taste: 'fruity',
//     chardonay: 92,
//     carmenere: 54,
//     syrah: 44,
//   },
//   {
//     taste: 'bitter',
//     chardonay: 28,
//     carmenere: 50,
//     syrah: 35,
//   },
//   {
//     taste: 'heavy',
//     chardonay: 106,
//     carmenere: 66,
//     syrah: 78,
//   },
//   {
//     taste: 'strong',
//     chardonay: 41,
//     carmenere: 87,
//     syrah: 80,
//   },
//   {
//     taste: 'sunny',
//     chardonay: 50,
//     carmenere: 35,
//     syrah: 109,
//   },
// ];
// const data1 = [
//   {
//     gradeMin: '0',
//     gradeMax: '10',
//     count: 0,
//     countColor: 'hsl(82, 70%, 50%)',
//   },
//   {
//     gradeMin: '10',
//     gradeMax: '20',
//     count: 0,
//     countColor: 'hsl(204, 70%, 50%)',
//   },
//   {
//     gradeMin: '20',
//     gradeMax: '30',
//     count: 0,
//     countColor: 'hsl(192, 70%, 50%)',
//   },
//   {
//     gradeMin: '30',
//     gradeMax: '40',
//     count: 0,
//     countColor: 'hsl(342, 70%, 50%)',
//   },
//   {
//     gradeMin: '40',
//     gradeMax: '50',
//     count: 0,
//     countColor: 'hsl(176, 70%, 50%)',
//   },
//   {
//     gradeMin: '50',
//     gradeMax: '60',
//     count: 0,
//     countColor: 'hsl(8, 70%, 50%)',
//   },
//   {
//     gradeMin: '60',
//     gradeMax: '70',
//     count: 0,
//     countColor: 'hsl(102, 70%, 50%)',
//   },
//   {
//     gradeMin: '70',
//     gradeMax: '80',
//     count: 0,
//     countColor: 'hsl(102, 70%, 50%)',
//   },
//   {
//     gradeMin: '80',
//     gradeMax: '90',
//     count: 0,
//     countColor: 'hsl(102, 70%, 50%)',
//   },
//   {
//     gradeMin: '90',
//     gradeMax: '100',
//     count: 0,
//     countColor: 'hsl(102, 70%, 50%)',
//   },
// ];
const POST_SUBMIT_ASSIGNMENT = 'Student/Courses/';
const colorStyle = (vl) => {
  if (vl < 18) return '#ff0d0d';
  if (vl < 36) return '#ff4e11';
  if (vl < 52) return '#ff8e15';
  if (vl < 70) return '#fab733';
  if (vl < 88) return '#acb334';
  if (vl < 100) return '#69b34c';
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

const CourseGrade = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  //   const [title, setTitle] = useState('');
  //   const [questions, setQuestions] = useState([]);
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
          `Instructor/Courses/${courseId}/${studentId}/StudentReports?semesterId=1`,
        );
        setTakeAssignments(response.data.assignmentGradesDtos);
        setStudent(response.data.student);
        setGrade({
          grade: response.data.grade,
          fullMarks: response.data.fullMarks,
        });

        var piData = {};
        for (var i in response.data.performanceIndicatorScores) {
          var piSc = response.data.performanceIndicatorScores[i];
          piData[piSc.name] = piData[piSc.name]
            ? piData[piSc.name]
            : { score: 0, fullMarks: piSc.fullMarks };
          piData[piSc.name].score += piSc.score;
        }
        console.log(piData);
        for (var i in piData) {
          piData[i].score = (piData[i].score / piData[i].fullMarks) * 100;
        }
        // console.log(piData);
        setPiScores(
          Object.keys(piData).map((el) => ({
            name: el,
            Score: piData[el].score,
          })),
        );
        console.log(response.data);
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
            <Box onClick={() => navigate(`${material.student.id}`)}>
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
            // onChange={(e) => setGapValue(e.target.value)}
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
        {/* {inputScores && */}
        {/* (
        <ResponsiveBar
          //   data={inputScores}
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
        />
        ) */}
      </Box>
    </Box>
  );
};

export default CourseGrade;

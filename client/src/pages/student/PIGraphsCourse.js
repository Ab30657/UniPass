import React, {
  useContext,
  useEffect,
  useState,
  useMemo,
  useRef,
  useCallback,
} from 'react';
import { Box, Typography } from '@mui/material';
import { Bar, Line } from 'react-chartjs-2';
import { useNavigate, Link, useParams } from 'react-router-dom';
import { ResponsiveRadar } from '@nivo/radar';
import { ResponsivePie } from '@nivo/pie';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import Header from '../../components/Header';
import { NumbersSharp } from '@mui/icons-material';

const GET_COURSE_URL = 'Student/Courses/';
const GET_ASSIGNMENT_GRADES_URL = 'Student/Assignment/';
const PerformanceIndicatorGraph = () => {
  //Get title for the courses
  const [Assignment, setAssignment] = useState([]);
  const [course, setCourse] = useState({ instructors: [] });

  //Extra for PI charts
  const [assignmentTake, setAssignmentTake] = useState({});
  const [userAnswers, setUserAnswers] = useState([]);
  const [questions, setQuestions] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  const [piScores, setPiScores] = useState([]);
  const [updatedpiScores, setUpdatedPiScores] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  const { courseId } = useParams();

  useEffect(() => {
    //To get header for instructor
    const fetchData = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(GET_COURSE_URL + courseId);
        console.log(response);
        setCourse(response.data);
      } catch (error) {
        console.log(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
    const number_assignments = [];
    const fetchData1 = async () => {
      showLoading();
      try {
        const res = await axiosPrivate.get(
          GET_COURSE_URL + courseId + '/Materials',
        );
        const data = res.data;
        setAssignment(data);

        // Fetch data for all assignments in parallel
        const assignmentResponses = await Promise.all(
          data.map((assignment) =>
            axiosPrivate.get(
              GET_ASSIGNMENT_GRADES_URL + assignment.id + '/grades',
            ),
          ),
        );

        let piScoreSums = {};
        let piScoreCounts = {};

        // Process results for each assignment
        assignmentResponses.forEach((response) => {
          response.data.performanceIndicatorScores.forEach((piScore) => {
            if (piScoreSums[piScore.name]) {
              piScoreSums[piScore.name] += piScore.score;
              piScoreCounts[piScore.name]++;
            } else {
              piScoreSums[piScore.name] = piScore.score;
              piScoreCounts[piScore.name] = 1;
            }
          });
        });
        console.log(piScoreSums);
        console.log(piScoreCounts);
        let averagePiScores = Object.keys(piScoreSums).map((piName) => ({
          name: piName,
          Score: piScoreSums[piName] / piScoreCounts[piName],
        }));

        setPiScores(averagePiScores);
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData1();
  }, []);

  return (
    <Box m="20px">
      <Header
        title={course?.title}
        subtitle={`Instructor: ${
          course?.instructors[0]?.firstName +
          ' ' +
          course?.instructors[0]?.lastName
        }`}
      />
      <Box
        display="flex"
        flexDirection="column"
        justifyContent="center"
        alignItems="center"
        borderBottom={`4px solid ${colors.primary[500]}`}
        p="15px"
        height="75vh"
      >
        <Typography color={colors.grey[100]} variant="h5" fontWeight="600">
          Performance Indicator Scores
        </Typography>
        {piScores.length >= 0 && (
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
  );
};

export default PerformanceIndicatorGraph;

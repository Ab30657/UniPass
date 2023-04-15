import React, { useContext, useEffect, useState } from 'react';
import { useTheme, Grid, Box, MenuItem, Button } from '@mui/material';
import TextField from '@mui/material/TextField';
import { Container } from '@mui/system';
import { tokens } from '../../theme';
import OutlinedInput from '@mui/material/OutlinedInput';
import Header from '../../components/Header';
import InputLabel from '@mui/material/InputLabel';
// import { MenuItem } from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import { ThemeProvider } from '@mui/material/styles';
import Select from '@mui/material/Select';
import { useParams } from 'react-router-dom';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import LoadingContext from '../../context/LoadingContext';

const GET_COURSE_BY_ID = 'instructor/courses';
const GET_ALL_PIs = 'instructor/PI';
const UPDATE_COURSE = 'instructor/course/';

const Editcourse = () => {
  const theme = useTheme();
  const ctheme = useTheme();
  const colors = tokens(theme.palette.mode);
  let { courseId } = useParams();
  const [Pi, setPi] = useState([]);
  const [allPIs, setAllPIs] = useState([]);
  const { showLoading, hideLoading } = useContext(LoadingContext);
  const [course, setCourse] = useState();
  const axiosPrivate = useAxiosPrivate();

  const getStyles = (piId, piList, customTheme) => {
    // console.log(piList.includes(piId));
    return {
      fontWeight: piList.includes(piId)
        ? theme.typography.fontWeightBold
        : theme.typography.fontWeightRegular,
    };
  };

  useEffect(() => {
    showLoading();
    Promise.all([
      axiosPrivate.get(GET_COURSE_BY_ID + `/${courseId}`),
      axiosPrivate.get(GET_ALL_PIs),
    ])
      .then(([reqCourseData, reqPiData]) => {
        const data = reqCourseData.data;
        // console.log(data);
        setCourse(data);
        //
        const piData = reqPiData.data;
        // console.log(piData);
        setAllPIs(piData);
        setPi(data.performanceIndicators.map((x) => x.id));
      })
      .finally(() => {
        hideLoading();
      });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    showLoading();
    // console.log();
    const response = axiosPrivate
      .put(UPDATE_COURSE + courseId, Pi)
      .then((response) => {
        // handle successful response
        // console.log(response.data);
        // navigate('/courses');
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
      <Header title="Edit Course" />
      <Box
        component="form"
        onSubmit={handleSubmit}
        sx={{
          marginTop: 15,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          minWidth: '120',
        }}
        noValidate
        autoComplete="off"
      >
        <Grid container spacing={3}>
          <Grid item xs={6} sm={6}>
            <TextField
              disabled
              fullWidth
              sx={{ color: colors.grey[700] }}
              id="CourseInstructor"
              label="Instructor"
              variant="filled"
              value={
                (course ? course.instructors[0].firstName : ' ') +
                ' ' +
                (course ? course.instructors[0].lastName : ' ')
              }
            />
          </Grid>
          <Grid item xs={6} sm={6}>
            <TextField
              disabled
              fullWidth
              sx={{ color: colors.grey[700] }}
              id="CourseTitle"
              label="Course Title"
              variant="filled"
              value={course ? course?.title : ''}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <FormControl fullWidth>
              <InputLabel
                variant="filled"
                id="demo-multiple-name-label"
                style={{ color: colors.greenAccent[500] }}
              >
                Performance Indicators
              </InputLabel>
              <Select
                // labelId="demo-multiple-name-label"
                // id="demo-multiple-name"
                fullWidth
                variant="filled"
                multiple
                value={Pi}
                onChange={(e) => setPi(e.target.value)}
                MenuProps={{
                  sx: {
                    '&& .Mui-selected': {
                      backgroundColor: colors.greenAccent[600],
                    },
                  },
                }}
                // fullWidth
              >
                {allPIs.map((pi) => {
                  return (
                    <MenuItem
                      key={pi.id}
                      value={pi.id}
                      style={getStyles(pi.id, Pi, ctheme)}
                    >
                      {pi.name}
                    </MenuItem>
                  );
                })}
              </Select>
            </FormControl>
          </Grid>
        </Grid>
        <Button
          sx={{
            backgroundColor: colors.blueAccent[700],
            color: colors.grey[100],
            fontSize: '14px',
            fontWeight: 'bold',
            padding: '10px 20px',
            boxShadow: 5,
            mt: 3,
            mb: 2,
          }}
          onClick={handleSubmit}
          type="submit"
          // fullWidth
          variant="contained"
          size="large"
          // sx={ {color: colors.grey[100],}}}
          // fullWidth
          // sx={{ mt: 3, mb: 2 }}
        >
          Save Changes
        </Button>
      </Box>
    </Box>
  );
};

export default Editcourse;

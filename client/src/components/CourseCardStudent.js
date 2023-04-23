import PropTypes from 'prop-types';
import ShieldCheckIcon from '@heroicons/react/24/solid/ShieldCheckIcon';
import SquaresPlusIcon from '@heroicons/react/24/solid/SquaresPlusIcon';
import {
  Alert,
  Box,
  Button,
  Card,
  CardContent,
  Divider,
  Snackbar,
  Stack,
  SvgIcon,
  Typography,
} from '@mui/material';
import { ClassOutlined } from '@mui/icons-material';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useNavigate } from 'react-router-dom';
import { useContext, useEffect, useState } from 'react';
import LoadingContext from '../context/LoadingContext';
import useAxiosPrivate from '../hooks/useAxiosPrivate';

const REGISTER_COURSE_URL = 'student/Courses/Register';

export const CourseCard = (props) => {
  const [open, setOpen] = useState(false);

  const handleClose = (event, reason) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const { course, hasRegistered } = props;
  const [registered, setRegistered] = useState();
  const semesterId = 1;
  const theme = useTheme();
  const navigate = useNavigate();
  const colors = tokens(theme.palette.mode);
  const { showLoading, hideLoading } = useContext(LoadingContext);
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    setRegistered(hasRegistered);
  }, [hasRegistered]);
  const registerCourse = () => {
    showLoading();
    // console.log();
    const response = axiosPrivate
      .post(
        REGISTER_COURSE_URL,
        JSON.stringify({ courseId: course.id, semesterId }),
      )
      .then((response) => {
        if (response.status === 200) {
          setOpen(true);
          setRegistered(true);
        }
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
    <Card
      sx={{
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
        backgroundColor: colors.primary[400],
      }}
    >
      <CardContent>
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'center',
            pb: 3,
          }}
        >
          <ClassOutlined
            sx={{ color: colors.greenAccent[600], fontSize: '25px' }}
          />
        </Box>
        <Typography
          align="center"
          color={colors.greenAccent[500]}
          gutterBottom
          variant="h3"
        >
          {course.title}
        </Typography>
        <Typography align="center" variant="body1">
          Computer Science (CS) is the study of computers and computational
          systems. Computer scientists create systems that are correct,
          reliable, and efficient.
        </Typography>
      </CardContent>
      <Box sx={{ flexGrow: 1 }} />
      <Divider />
      <Stack
        alignItems="center"
        direction="row"
        justifyContent="space-between"
        divider={<Divider orientation="vertical" flexItem />}
        spacing={2}
        sx={{ p: 2 }}
      >
        <Stack alignItems="center" direction="row" spacing={1}>
          {registered && (
            <Button onClick={() => navigate(`/Courses/${course.id}/Materials`)}>
              <SvgIcon fontSize="small">
                <ShieldCheckIcon color={colors.greenAccent[400]} />
              </SvgIcon>
              <Typography
                color={colors.greenAccent[400]}
                display="inline"
                variant="body2"
              >
                View Course
              </Typography>
            </Button>
          )}
          {!registered && (
            <Button onClick={registerCourse}>
              <SvgIcon fontSize="small">
                <SquaresPlusIcon color={colors.redAccent[400]} />
              </SvgIcon>
              <Typography
                color={colors.redAccent[400]}
                display="inline"
                variant="body2"
              >
                Register
              </Typography>
            </Button>
          )}
        </Stack>
      </Stack>
      <Snackbar open={open} autoHideDuration={6000} onClose={handleClose}>
        <Alert onClose={handleClose} severity="success" sx={{ width: '100%' }}>
          Registration Successful
        </Alert>
      </Snackbar>
    </Card>
  );
};

CourseCard.propTypes = {
  course: PropTypes.object.isRequired,
};

import PropTypes from 'prop-types';
import ArrowDownOnSquareIcon from '@heroicons/react/24/solid/ArrowDownOnSquareIcon';
import ClockIcon from '@heroicons/react/24/solid/ClockIcon';
import ViewFinderCircle from '@heroicons/react/24/solid/ViewfinderCircleIcon';
import {
  Avatar,
  Box,
  Button,
  Card,
  CardContent,
  Divider,
  Stack,
  SvgIcon,
  Typography,
} from '@mui/material';
import { ClassOutlined, ViewAgenda } from '@mui/icons-material';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { bgcolor, width } from '@mui/system';
import { useNavigate } from 'react-router-dom';

export const CourseCard = (props) => {
  const { course } = props;
  const theme = useTheme();
  const navigate = useNavigate();
  const colors = tokens(theme.palette.mode);
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
          <Button onClick={() => navigate(`${course.id}`)}>
            <SvgIcon fontSize="small">
              <ClockIcon color={colors.greenAccent[400]} />
            </SvgIcon>
            <Typography
              color={colors.greenAccent[400]}
              display="inline"
              variant="body2"
            >
              Edit
            </Typography>
          </Button>
          <Button onClick={() => navigate(`${course.id}/Materials`)}>
            <SvgIcon fontSize="small">
              <ViewFinderCircle color={colors.greenAccent[400]} />
            </SvgIcon>
            <Typography
              color={colors.greenAccent[400]}
              display="inline"
              variant="body2"
            >
              View
            </Typography>
          </Button>
        </Stack>
      </Stack>
    </Card>
  );
};

CourseCard.propTypes = {
  course: PropTypes.object.isRequired,
};

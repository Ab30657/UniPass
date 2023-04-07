import PropTypes from 'prop-types';
import ArrowDownOnSquareIcon from '@heroicons/react/24/solid/ArrowDownOnSquareIcon';
import ClockIcon from '@heroicons/react/24/solid/ClockIcon';
import {
  Avatar,
  Box,
  Card,
  CardContent,
  Divider,
  Stack,
  SvgIcon,
  Typography,
} from '@mui/material';
import { ClassOutlined } from '@mui/icons-material';

export const CourseCard = (props) => {
  const { course } = props;

  return (
    <Card
      sx={{
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
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
          <Avatar variant="square">
            <ClassOutlined />
          </Avatar>
        </Box>
        <Typography align="center" gutterBottom variant="h5">
          {course.title}
        </Typography>
        <Typography align="center" variant="body1">
          Computer Science (CS) is the study of computers and computational
          systems. Computer scientists create systems that are correct,
          reliable, and efficient.The department currently offers two track
          options which are the Computer Science-Computer Science (CS-CS) and
          Computer Science-Software Development (CS-SD).
        </Typography>
      </CardContent>
      <Box sx={{ flexGrow: 1 }} />
      <Divider />
      <Stack
        alignItems="center"
        direction="row"
        justifyContent="space-between"
        spacing={2}
        sx={{ p: 2 }}
      >
        <Stack alignItems="center" direction="row" spacing={1}>
          <SvgIcon color="action" fontSize="small">
            <ClockIcon />
          </SvgIcon>
          <Typography color="text.secondary" display="inline" variant="body2">
            Updated 2hr ago
          </Typography>
        </Stack>
        <Stack alignItems="center" direction="row" spacing={1}>
          <SvgIcon color="action" fontSize="small">
            <ArrowDownOnSquareIcon />
          </SvgIcon>
          <Typography color="text.secondary" display="inline" variant="body2">
            {course.downloads} Downloads
          </Typography>
        </Stack>
      </Stack>
    </Card>
  );
};

CourseCard.propTypes = {
  course: PropTypes.object.isRequired,
};

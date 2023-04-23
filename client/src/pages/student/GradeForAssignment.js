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
} from '@mui/material';
import { Box } from '@mui/system';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import LoadingContext from '../../context/LoadingContext';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';

const GradeForAssignment = () => {
  const [assignment, setAssignment] = useState({});
  const [userAnswers, setUserAnswers] = useState([]);
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [title, setTitle] = useState('');
  const [questions, setQuestions] = useState([]);
  const axiosPrivate = useAxiosPrivate();
  const { showLoading, hideLoading } = useContext(LoadingContext);

  const { courseId, assignmentId } = useParams();

  useEffect(() => {
    const fetchData = async () => {
      showLoading();
      try {
        const response = await axiosPrivate.get(
          `Student/Assignment/${assignmentId}/grades`,
        );
        //testing
        console.log(response.data);
        setAssignment(response.data);
        setTitle(response.data.title);
        setQuestions(response.data.questions);
        // setTakeAssignments(response.data.questions);
      } catch (error) {
        console.error(error);
      } finally {
        hideLoading();
      }
    };
    fetchData();
  }, []);
};

export default GradeForAssignment;

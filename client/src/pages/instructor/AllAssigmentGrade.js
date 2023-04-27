import React, { useContext, useEffect, useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import Header from '../../components/Header';
import Box from '@mui/material/Box';
import { Container, Stack } from '@mui/system';

const AllAssigmentGrade = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [grade, setgrade] = useState('');
  const data = [
    {
      title: 'string',
      student: {
        id: 0,
        firstName: 'string',
        lastName: 'string',
        appUserId: 0,
      },
      grade: 0,
      fullMarks: 0,
      performanceIndicatorScores: [
        {
          id: 0,
          name: 'string',
          score: 0,
          fullMarks: 0,
        },
      ],
      takeQuestions: [
        {
          id: 0,
          questionId: 0,
          answerText: 'string',
          correct: true,
          correctAnswer: 'string',
        },
      ],
    },
    {
      title: 'string1',
      student: {
        id: 0,
        firstName: 'string1',
        lastName: 'string1',
        appUserId: 0,
      },
      grade: 0,
      fullMarks: 0,
      performanceIndicatorScores: [
        {
          id: 0,
          name: 'string1',
          score: 0,
          fullMarks: 0,
        },
      ],
      takeQuestions: [
        {
          id: 0,
          questionId: 0,
          answerText: 'string1',
          correct: true,
          correctAnswer: 'string1',
        },
      ],
    },
  ];
  return (
    <Box
      component="form"
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        minWidth: '120',
      }}
      noValidate
      autoComplete="off"
    >
      <Container maxWidth="lg">
        <Header title="View All Grades" />
      </Container>
    </Box>
  );
};
export default AllAssigmentGrade;

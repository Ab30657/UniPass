import React from 'react';
import { Box, Typography } from '@mui/material';
import { Bar } from 'react-chartjs-2';

const PerformanceIndicatorGraph = ({ questions }) => {
  const performanceIndicators = questions.flatMap((question) =>
    question.performanceIndicators.map((indicator) => ({
      ...indicator,
      questionId: question.id,
    })),
  );

  const data = {
    labels: performanceIndicators.map(
      (indicator) => `Q${indicator.questionId}: ${indicator.name}`,
    ),
    datasets: [
      {
        label: 'Performance Indicators',
        data: performanceIndicators.map((indicator) => indicator.value || 0),
        backgroundColor: 'rgba(75, 192, 192, 0.2)',
        borderColor: 'rgba(75, 192, 192, 1)',
        borderWidth: 1,
      },
    ],
  };

  const options = {
    scales: {
      y: {
        beginAtZero: true,
        ticks: {
          stepSize: 1,
        },
      },
    },
  };

  return (
    <Box>
      <Typography variant="h6" gutterBottom>
        Performance Indicators
      </Typography>
      <Bar data={data} options={options} />
    </Box>
  );
};

export default PerformanceIndicatorGraph;

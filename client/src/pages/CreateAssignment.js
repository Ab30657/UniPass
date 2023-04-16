import React, { useContext, useEffect, useState } from 'react';
import { Container, Stack } from '@mui/system';
import Box from '@mui/material/Box';
import Header from '../components/Header';
import TextField from '@mui/material/TextField';
import { Button, Grid, SvgIcon, Typography } from '@mui/material';
import { useTheme } from '@emotion/react';
import IconButton from '@mui/material/IconButton';
import RemoveIcon from '@mui/icons-material/Remove';
import AddIcon from '@mui/icons-material/Add';
import { tokens } from '../theme';
const CreateAssignment = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [question, setQuestion] = useState([
    { Description: '', Option1: '', Option2: '', Option3: '', Option4: '' },
  ]);
  const handleChange = (index, event) => {
    const values = [...question];
    values[index][event.target.name] = event.target.value;
    setQuestion(values);
    // console.log(index, event.target.name);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Question', question);
  };
  const handleAdd = () => {
    setQuestion([
      ...question,
      { Description: '', Option1: '', Option2: '', Option3: '', Option4: '' },
    ]);
  };
  const handleRemove = (index) => {
    const values = [...question];
    values.splice(index, 1);
    setQuestion(values);
  };

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
      <Container maxWidth="md">
        <Header title="Create Assignments" />
        {question.map((questions, index) => (
          <div key={index}>
            <Stack spacing={3} sx={{ alignItems: 'center' }}>
              <TextField
                name="Description"
                label="Description"
                variant="filled"
                value={questions.Description}
                fullWidth
                onChange={(event) => handleChange(index, event)}
              ></TextField>

              <TextField
                name="Option1"
                label="Option1"
                variant="filled"
                value={questions.Option1}
                fullWidth
                onChange={(event) => handleChange(index, event)}
              ></TextField>
              <TextField
                name="Option2"
                label="Option2"
                variant="filled"
                value={questions.Option2}
                fullWidth
                onChange={(event) => handleChange(index, event)}
              ></TextField>
              <TextField
                name="Option3"
                label="Option3"
                variant="filled"
                value={questions.Option3}
                fullWidth
                onChange={(event) => handleChange(index, event)}
              ></TextField>
              <TextField
                name="Option4"
                label="Option4"
                variant="filled"
                value={questions.Option4}
                fullWidth
                onChange={(event) => handleChange(index, event)}
              ></TextField>
              <Stack direction="row" spacing={5}>
                <IconButton onClick={() => handleRemove(index)}>
                  <RemoveIcon />
                </IconButton>
                <IconButton onClick={() => handleAdd()}>
                  <AddIcon />
                </IconButton>
              </Stack>
            </Stack>
          </div>
        ))}
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
          fullWidth
          type="submit"
          onClick={handleSubmit}
        >
          Create Assignment
        </Button>
      </Container>
    </Box>
  );
};
export default CreateAssignment;

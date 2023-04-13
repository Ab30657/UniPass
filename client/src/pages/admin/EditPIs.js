import { Button, CircularProgress, Fab } from '@mui/material';
import { useState } from 'react';
import { Check, Save } from '@mui/icons-material';
import { Box } from '@mui/system';

function EditPIs() {
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const handleSubmit = async () => {};
  return (
    <Box
      sx={{
        m: 1,
        position: 'relative',
      }}
    >
      <Button variant="contained" color="success">
        Edit
      </Button>
    </Box>
  );
}

export default EditPIs;

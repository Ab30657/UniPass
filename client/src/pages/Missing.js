import Header from './Header';
import { ArrowLeftOutlined } from '@mui/icons-material';
import { Box, Button, Container, SvgIcon, Typography } from '@mui/material';
import notfound from '../assets/error-404.png';
const Missing = () => {
  return (
    <>
      <Header>
        <title>404 | Devias Kit</title>
      </Header>
      <Box
        sx={{
          alignItems: 'center',
          display: 'flex',
          flexGrow: 1,
          minHeight: '100%',
        }}
      >
        <Container maxWidth="md">
          <Box
            sx={{
              alignItems: 'center',
              display: 'flex',
              flexDirection: 'column',
            }}
          >
            <Box
              sx={{
                mb: 3,
                textAlign: 'center',
              }}
            >
              <img
                alt="Under development"
                src={notfound}
                style={{
                  display: 'inline-block',
                  maxWidth: '100%',
                  width: 400,
                }}
              />
            </Box>
            <Typography align="center" sx={{ mb: 3 }} variant="h3">
              404: The page you are looking for isn’t here
            </Typography>
            <Typography align="center" color="text.secondary" variant="body1">
              You either tried some shady route or you came here by mistake.
              Whichever it is, try using the navigation
            </Typography>
            <Button
              href="/"
              startIcon={
                <SvgIcon fontSize="small">
                  <ArrowLeftOutlined />
                </SvgIcon>
              }
              sx={{ mt: 3 }}
              variant="contained"
            >
              Go back to dashboard
            </Button>
          </Box>
        </Container>
      </Box>
    </>
  );
};

export default Missing;

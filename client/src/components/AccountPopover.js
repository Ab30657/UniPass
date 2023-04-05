import { useCallback, useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import AuthContext from '../context/AuthProvider';
import {
  Box,
  Divider,
  MenuItem,
  MenuList,
  Popover,
  Typography,
} from '@mui/material';
import useAuth from '../hooks/useAuth';
import { useNavigate } from 'react-router-dom';

export const AccountPopover = (props) => {
  const { anchorEl, onClose, open } = props;
  const { auth, setAuth } = useAuth(AuthContext);
  const { loggedIn, setLoggedIn } = useState(true);
  const navigate = useNavigate();

  const handleSignOut = useCallback(() => {
    localStorage.removeItem('user');
    setAuth(null);
    navigate('/login', { replace: true });
  });

  return (
    <Popover
      anchorEl={anchorEl}
      anchorOrigin={{
        horizontal: 'left',
        vertical: 'bottom',
      }}
      onClose={onClose}
      open={open}
      PaperProps={{ sx: { width: 200 } }}
    >
      <Box
        sx={{
          py: 1.5,
          px: 2,
        }}
      >
        <Typography variant="overline">Account</Typography>
        <Typography color="text.secondary" variant="body2">
          {auth?.name}
        </Typography>
      </Box>
      <Divider />
      <MenuList
        disablePadding
        dense
        sx={{
          p: '8px',
          '& > *': {
            borderRadius: 1,
          },
        }}
      >
        <MenuItem onClick={handleSignOut}>Sign out</MenuItem>
      </MenuList>
    </Popover>
  );
};

AccountPopover.propTypes = {
  anchorEl: PropTypes.any,
  onClose: PropTypes.func,
  open: PropTypes.bool.isRequired,
};

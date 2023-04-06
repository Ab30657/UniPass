import React, { useContext } from 'react';
import { Backdrop, CircularProgress } from '@mui/material';
import LoadingContext from '../context/LoadingContext';
import { InfinitySpin } from 'react-loader-spinner';

const Spinner = () => {
  const { loadingCount } = useContext(LoadingContext);

  return (
    <>
      {loadingCount > 0 && (
        <Backdrop
          sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
          open={true}
        >
          <InfinitySpin width="200" color="#4fa94d" />
          {/* <CircularProgress color="inherit" /> */}
        </Backdrop>
        // <div className="loading-wrapper">
        // </div>
      )}
    </>
  );
};

export default Spinner;

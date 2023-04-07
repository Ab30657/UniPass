import { axiosPrivate } from '../api/axios';
import { useContext, useEffect } from 'react';
//import useAuth from './useAuth';
import AuthContext from '../context/AuthContext';

const useAxiosPrivate = () => {
  //const { auth } = useAuth();
  const { user } = useContext(AuthContext);
  useEffect(() => {
    const requestIntercept = axiosPrivate.interceptors.request.use(
      (config) => {
        if (!config.headers['Authorization']) {
          config.headers['Authorization'] = `Bearer ${user?.token}`;
        }
        return config;
      },
      (error) => Promise.reject(error),
    );

    return () => {
      axiosPrivate.interceptors.request.eject(requestIntercept);
    };
  }, [user]);

//   return axiosPrivate;
// };

// export default useAxiosPrivate;

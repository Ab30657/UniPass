import { useLocation, Navigate, Outlet } from 'react-router-dom';
import AuthContext from '../context/AuthContext';
import { useContext } from 'react';

const RequireAuth = ({ allowedRoles }) => {
  const authContext = useContext(AuthContext);
  const location = useLocation();
  return authContext?.user?.roles?.find((role) =>
    allowedRoles?.includes(role),
  ) ? (
    <Outlet />
  ) : authContext?.user?.token ? (
    <Navigate to="/unauthorized" state={{ from: location }} replace />
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};

export default RequireAuth;

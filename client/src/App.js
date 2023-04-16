import './App.css';
import { Routes, Route, Navigate } from 'react-router-dom';
import Signup from './pages/SignUp';
import Login from './pages/Login';
import Layout from './pages/Layout';
import RequireAuth from './components/RequireAuth';
import Dashboard from './pages/Dashboard';
import Missing from './pages/Missing';
import AuthContext from './context/AuthContext';
import { CssBaseline, ThemeProvider } from '@mui/material';
import { ColorModeContext, useMode } from './theme';
import { useCallback, useEffect, useState } from 'react';
import Courses from './pages/Courses';
import PerformanceIndicators from './pages/PIs';
import LoadingProvider from './components/LoadingProvider';
import Spinner from './components/Spinner';
import Createcourse from './pages/Createcourse';
import Users from './pages/Users';
import ListAssignment from './pages/ListAssignment';
import CreateAssignment from './pages/CreateAssignment';
//import { createTheme } from './theme';
import CreatePIs from './pages/CreatePIs';

const ROLES = {
  0: 'Admin',
  1: 'Student',
  2: 'Instructor',
};

function App() {
  const [theme, colorMode] = useMode();
  const [user, setUser] = useState(JSON.parse(localStorage.getItem('user')));

  const login = useCallback((user) => {
    setUser(user);
    localStorage.setItem('user', JSON.stringify(user));
  }, []);

  const logout = useCallback(() => {
    setUser(null);
    localStorage.removeItem('user');
  }, []);

  useEffect(() => {
    const storedData = JSON.parse(localStorage.getItem('user'));
    if (storedData && storedData.token) {
      login(storedData);
    }
  }, [login]);

  //   let user = localStorage.getItem('user');
  //   user = JSON.parse(user);
  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: !!user,
        user: user,
        login: login,
        logout: logout,
      }}
    >
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme}>
          <div className="app">
            <CssBaseline />
            <LoadingProvider>
              <Spinner />
              <Routes>
                <Route
                  path="login"
                  element={user ? <Navigate to="/Dashboard" /> : <Login />}
                />
                <Route
                  path="signup"
                  element={user ? <Navigate to="/Dashboard" /> : <Signup />}
                />
                <Route element={<Layout />}>
                  <Route
                    path="/"
                    exact
                    element={<Navigate to="/Dashboard" />}
                  />
                  <Route
                    element={
                      <RequireAuth
                        allowedRoles={[ROLES[1], ROLES[2], ROLES[0]]}
                      />
                    }
                  >
                    <Route path="/Dashboard" element={<Dashboard />} />
                    <Route path="/Users" element={<Users />} />
                    <Route path="/Courses" element={<Courses />} />
                    <Route path="/PIs" element={<PerformanceIndicators />} />
                    <Route
                      path="/ListAssignment"
                      element={<ListAssignment />}
                    />
                    <Route
                      path="/CreateAssignment"
                      element={<CreateAssignment />}
                    />
                  </Route>
                  <Route element={<RequireAuth allowedRoles={[ROLES[0]]} />}>
                    <Route path="/Courses/New" element={<Createcourse />} />
                  </Route>
                  <Route element={<RequireAuth allowedRoles={[ROLES[0]]} />}>
                    <Route path="/PIs/Create" element={<CreatePIs />} />
                  </Route>
                </Route>
                <Route path="*" element={<Missing />} />
              </Routes>
            </LoadingProvider>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </AuthContext.Provider>
  );
}

export default App;

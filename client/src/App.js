import './App.css';
import { Routes, Route, Navigate } from 'react-router-dom';
import Signup from './pages/SignUp';
import Login from './pages/Login';
import Layout from './pages/Layout';
import RequireAuth from './components/RequireAuth';
import Dashboard from './pages/Dashboard';
import Missing from './pages/Missing';
import { AuthProvider } from './context/AuthProvider';
import { CssBaseline, ThemeProvider } from '@mui/material';
import { ColorModeContext, useMode } from './theme';

const ROLES = {
  0: 'Admin',
  1: 'Student',
  2: 'Instructor',
};

function App() {
  const [theme, colorMode] = useMode();
  let user = localStorage.getItem('user');
  user = JSON.parse(user);
  return (
    <AuthProvider user={user}>
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme}>
          <div className="app">
            <CssBaseline />
            <Routes>
              <Route
                path="login"
                element={user ? <Navigate to="/Dashboard" /> : <Login />}
              />
              <Route
                path="signup"
                element={user ? <Navigate to="/Dashboard" /> : <Signup />}
              />
              <Route path="/" exact element={<Navigate to="/Dashboard" />} />
              <Route
                element={
                  <RequireAuth allowedRoles={[ROLES[1], ROLES[2], ROLES[0]]} />
                }
              >
                <Route element={<Layout />}>
                  <Route path="/Dashboard" element={<Dashboard />} />
                </Route>
                <Route path="*" element={<Missing />} />
              </Route>
            </Routes>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </AuthProvider>
  );
}

export default App;

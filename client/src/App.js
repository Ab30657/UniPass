import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Signup from './pages/SignUp';
import Login from './pages/Login';
import Layout from './pages/Layout';
import RequireAuth from './pages/RequireAuth';
import Dashboard from './pages/Dashboard';
import Missing from './pages/Missing';
import { AuthProvider } from './context/AuthProvider';
// import Dashboardpage from './pages/Dashboardpage';

const ROLES = {
  0: 'Admin',
  1: 'Student',
  2: 'Instructor',
};

function App() {
  //   console.log(process.env.REACT_APP_API_URL + '/WeatherForecast');
  //   fetch(process.env.REACT_APP_API_URL + '/WeatherForecast')
  //     .then((x) => x.json())
  //     .then((x) => console.log(x));
  let user = localStorage.getItem('user');
  user = JSON.parse(user);
  return (
    <AuthProvider user={user}>
      <Routes>
        <Route path="/" element={<Layout />}>
          {/* public routes */}
          <Route
            path="login"
            element={user ? <Navigate to="/Dashboard" /> : <Login />}
          />
          <Route
            path="signup"
            element={user ? <Navigate to="/Dashboard" /> : <Signup />}
          />

          {/* we want to protect these routes */}

          <Route element={<RequireAuth allowedRoles={[ROLES[1]]} />}>
            <Route path="/Dashboard" element={<Dashboard />} />
          </Route>

          {/* <Route element={<RequireAuth allowedRoles={[ROLES.Editor]} />}>
          <Route path="editor" element={<Editor />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
          <Route path="admin" element={<Admin />} />
        </Route> */}

          {/* catch all */}
          <Route path="*" element={<Missing />} />
        </Route>
        {/* Other routes */}
      </Routes>
    </AuthProvider>
  );
}

export default App;

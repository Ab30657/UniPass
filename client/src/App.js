import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Signup from './pages/SignUp';
import SignIn from './pages/SignIn';
// import Homepage from './pages/Homepage';
import Home from './pages/Home';
import Users from './pages/Users';

function App() {
  console.log(process.env.REACT_APP_API_URL + '/WeatherForecast');
  fetch(process.env.REACT_APP_API_URL + '/WeatherForecast')
    .then((x) => x.json())
    .then((x) => console.log(x));
  return (
    // <div className="App">
    <BrowserRouter>
      <Routes>
        <Route exact path="/SignUp" element={<Signup />} />
        <Route exact path="/SignIn" element={<SignIn />} />
        <Route exact path="/Homepage" element={<Home />} />
        <Route exact path="/Users" element={<Users />} />
        {/* Other routes */}
      </Routes>
    </BrowserRouter>
    // </div>
  );
}

export default App;

import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Signup from './pages/SignUp';

function App() {
  console.log(process.env.REACT_APP_API_URL + '/WeatherForecast');
  fetch(process.env.REACT_APP_API_URL + '/WeatherForecast')
    .then((x) => x.json())
    .then((x) => console.log(x));
  return (
    // <div className="App">
    <BrowserRouter>
      <Routes>
        <Route exact path="/signup" element={<Signup />} />
        {/* Other routes */}
      </Routes>
    </BrowserRouter>
    // </div>
  );
}

export default App;

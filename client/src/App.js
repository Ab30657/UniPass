import React from 'react';
import Createcourse from './pages/Createcourse';
// import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

function App() {
  // console.log(process.env.REACT_APP_API_URL + '/WeatherForecast');
  // fetch(process.env.REACT_APP_API_URL + '/WeatherForecast')
  //   .then((x) => x.json())
  //   .then((x) => console.log(x));
  return (
    <div className="App">
      <Createcourse />
    </div>
  );
}

export default App;

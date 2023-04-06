// import logo from './logo.svg';
// import './App.css';
import editCourse from './pages/editCourse';

function App() {
  console.log(process.env.REACT_APP_API_URL + '/WeatherForecast');
  fetch(process.env.REACT_APP_API_URL + '/WeatherForecast')
    .then((x) => x.json())
    .then((x) => console.log(x));
  return (
    <div className="App">
      <editCourse></editCourse>
    </div>
  );
}

export default App;

import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import Chart from 'react-apexcharts';
import ReactApexChart from 'react-apexcharts';
import Navbar from '../components/NavBar';
import UsersTable from '../components/UsersTable';

function Users() {
  const [data, setData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      fetch(process.env.REACT_APP_API_URL + '/Admin/Users')
        .then((x) => x.json())
        .then((x) => {
          if (data) setData(x);
        });
    };

    fetchData();
  }, []);
  //   const series = [
  //     {
  //       name: 'Profits',
  //       data: [
  //         37, 35, 44, 28, 36, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53,
  //         61, 27, 54, 43, 19, 46, 39, 62, 51, 35, 41, 67,
  //       ],
  //     },
  //   ];

  //   const options = {
  //     chart: {
  //       type: 'area',
  //       fontFamily: 'inherit',
  //       height: 40.0,
  //       sparkline: {
  //         enabled: true,
  //       },
  //       animations: {
  //         enabled: false,
  //       },
  //     },
  //     dataLabels: {
  //       enabled: false,
  //     },
  //     fill: {
  //       opacity: 0.16,
  //       type: 'solid',
  //     },
  //     stroke: {
  //       width: 2,
  //       lineCap: 'round',
  //       curve: 'smooth',
  //     },
  //     tooltip: {
  //       theme: 'dark',
  //     },
  //     grid: {
  //       strokeDashArray: 4,
  //     },
  //     xaxis: {
  //       labels: {
  //         padding: 0,
  //       },
  //       tooltip: {
  //         enabled: false,
  //       },
  //       axisBorder: {
  //         show: false,
  //       },
  //       type: 'datetime',
  //     },
  //     yaxis: {
  //       labels: {
  //         padding: 4,
  //       },
  //     },
  //     labels: [
  //       '2020-06-20',
  //       '2020-06-21',
  //       '2020-06-22',
  //       '2020-06-23',
  //       '2020-06-24',
  //       '2020-06-25',
  //       '2020-06-26',
  //       '2020-06-27',
  //       '2020-06-28',
  //       '2020-06-29',
  //       '2020-06-30',
  //       '2020-07-01',
  //       '2020-07-02',
  //       '2020-07-03',
  //       '2020-07-04',
  //       '2020-07-05',
  //       '2020-07-06',
  //       '2020-07-07',
  //       '2020-07-08',
  //       '2020-07-09',
  //       '2020-07-10',
  //       '2020-07-11',
  //       '2020-07-12',
  //       '2020-07-13',
  //       '2020-07-14',
  //       '2020-07-15',
  //       '2020-07-16',
  //       '2020-07-17',
  //       '2020-07-18',
  //       '2020-07-19',
  //     ],
  //     legend: {
  //       show: false,
  //     },
  //   };

  const token = Cookies.get('jwt');
  if (!token) {
    // redirect to the login page if the token doesn't exist
    window.location.href = '/SignIn';
    return null;
  }

  return (
    <div classname="page">
      <Navbar />
      <div className="page-wrapper">
        <div className="page-header d-print-none">
          <div className="container-xl">
            <div className="row g-2 align-items-center">
              <div className="col">
                <div className="page-pretitle">Admin</div>
                <h2 className="page-title">Dashboard</h2>
              </div>
            </div>
          </div>
        </div>
        <div className="page-body">
          <div className="container-xl">
            <div className="row row-cards">
              <UsersTable data={data} />
            </div>
          </div>
        </div>
        <footer className="footer footer-transparent d-print-none">
          <div className="container-xl">
            <div className="row text-center align-items-center flex-row-reverse">
              <div className="col-lg-auto ms-lg-auto">
                <ul className="list-inline list-inline-dots mb-0">
                  <li className="list-inline-item">
                    <a href="./docs/" className="link-secondary">
                      Documentation
                    </a>
                  </li>
                  <li className="list-inline-item">
                    <a href="./license.html" className="link-secondary">
                      License
                    </a>
                  </li>
                  <li className="list-inline-item">
                    <a
                      href="https://github.com/tabler/tabler"
                      target="_blank"
                      className="link-secondary"
                      rel="noopener"
                    >
                      Source code
                    </a>
                  </li>
                  <li className="list-inline-item">
                    <a
                      href="https://github.com/sponsors/codecalm"
                      target="_blank"
                      className="link-secondary"
                      rel="noopener"
                    >
                      {/* Download SVG icon from http://tabler-icons.io/i/heart */}
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        className="icon text-pink icon-filled icon-inline"
                        width={24}
                        height={24}
                        viewBox="0 0 24 24"
                        strokeWidth={2}
                        stroke="currentColor"
                        fill="none"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                      >
                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                        <path d="M19.5 12.572l-7.5 7.428l-7.5 -7.428a5 5 0 1 1 7.5 -6.566a5 5 0 1 1 7.5 6.572" />
                      </svg>
                      Sponsor
                    </a>
                  </li>
                </ul>
              </div>
              <div className="col-12 col-lg-auto mt-3 mt-lg-0">
                <ul className="list-inline list-inline-dots mb-0">
                  <li className="list-inline-item">
                    Copyright © 2023
                    <a href="." className="link-secondary">
                      Tabler
                    </a>
                    . All rights reserved.
                  </li>
                  <li className="list-inline-item">
                    <a
                      href="./changelog.html"
                      className="link-secondary"
                      rel="noopener"
                    >
                      v1.0.0-beta17
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </footer>
      </div>
    </div>
  );
}

export default Users;

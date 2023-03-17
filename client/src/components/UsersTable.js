import React, { useState } from 'react';
import Cookies from 'js-cookie';
import Chart from 'react-apexcharts';
import ReactApexChart from 'react-apexcharts';
import Navbar from './NavBar';

function UsersTable(props) {
  const { data } = props;
  return (
    <div className="col-12">
      <div className="card">
        <div className="table-responsive">
          <table className="table table-vcenter card-table table-striped">
            <thead>
              <tr>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Username</th>
                <th>Email</th>
                <th>Role</th>
                <th className="w-1" />
              </tr>
            </thead>
            <tbody>
              {data.map((row, index) => (
                <tr key={index}>
                  <td>{row.firstName}</td>
                  <td>{row.lastName}</td>
                  <td className="text-muted">{row.username}</td>
                  <td className="text-muted">
                    <a href="#" className="text-reset">
                      {row.email}
                    </a>
                  </td>
                  <td className="text-muted">{row.Role}</td>
                  <td>
                    <a href="#">Edit</a>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default UsersTable;

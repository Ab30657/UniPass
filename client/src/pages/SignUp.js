import React, { useState } from 'react';
import Cookies from 'js-cookie';

function SignUp() {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        firstName,
        lastName,
        username,
        email,
        password,
        role,
      }),
    };
    console.log(requestOptions.body);
    fetch(process.env.REACT_APP_API_URL + '/account/register', requestOptions)
      .then((x) => x.json())
      .then((user) => {
        //register
        console.log(user);
        //JWT Token expires in 7 days, and is available to all paths of the site
        Cookies.set('jwt', user.token, { expires: 7, path: '/' });
        //save token to cache
      });
    // handle form submission logic here
  };

  return (
    <div className="page page-center">
      <div className="container container-tight py-4">
        <div className="text-center mb-4">
          <a href="." className="navbar-brand navbar-brand-autodark">
            <img
              src="https://preview.tabler.io/static/logo.svg"
              height="36"
              alt=""
            />
          </a>
        </div>
        <form
          className="card card-md"
          onSubmit={handleSubmit}
          autoComplete="off"
          noValidate
        >
          <div className="card-body">
            <h2 className="card-title text-center mb-4">Create new account</h2>
            <div className="row mb-3">
              <div className="col-md-6">
                <label className="form-label">First Name</label>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter first name"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                />
              </div>
              <div className="col-md-6">
                <label className="form-label">Last Name</label>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter last name"
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                />
              </div>
            </div>
            <div className="mb-3">
              <label className="form-label">Username</label>
              <input
                type="text"
                className="form-control"
                placeholder="Enter username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Email address</label>
              <input
                type="email"
                className="form-control"
                placeholder="Enter email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Password</label>
              <div className="input-group input-group-flat">
                <input
                  type="password"
                  className="form-control"
                  placeholder="Password"
                  autoComplete="off"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
                <span className="input-group-text">
                  <a
                    href="./"
                    className="link-secondary"
                    title="Show password"
                    data-bs-toggle="tooltip"
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      className="icon"
                      width="24"
                      height="24"
                      viewBox="0 0 24 24"
                      strokeWidth="2"
                      stroke="currentColor"
                      fill="none"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    >
                      <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                      <path d="M12 12m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
                      <path d="M22 12c-2.667 4.667 -6 7 -10 7s-7.333 -2.333 -10 -7c2.667 -4.667 6 -7 10 -7s7.333 2.333 10 7" />
                    </svg>
                  </a>
                </span>
              </div>
            </div>
            <div className="mb-3">
              <label className="form-label">Role</label>
              <select
                className="form-select"
                value={role}
                onChange={(e) => setRole(e.target.value)}
              >
                <option value="" disabled>
                  Select role
                </option>
                <option value="Instructor">Instructor</option>
                <option value="Student">Student</option>
              </select>
            </div>
            <div className="mb-3">
              <label className="form-check">
                <input type="checkbox" className="form-check-input" />
                <span className="form-check-label">
                  Agree the{' '}
                  <a href="./terms-of-service.html" tabIndex="-1">
                    terms and policy
                  </a>
                  .
                </span>
              </label>
            </div>
            <div className="form-footer">
              <button type="submit" className="btn btn-primary w-100">
                Create new account
              </button>
            </div>
          </div>
        </form>
        <div className="text-center text-muted mt-3">
          Already have account?{' '}
          <a href="./sign-in.html" tabIndex="-1">
            Sign in
          </a>
        </div>
      </div>
    </div>
  );
}

export default SignUp;

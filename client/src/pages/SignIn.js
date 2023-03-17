import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import { Navigate, Route, useNavigate } from 'react-router-dom';

function SignIn() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const [isLoggedIn, setisLoggedIn] = useState(false);
  //   const [rememberMe, setRememberMe] = useState(false);

  useEffect(() => {
    // Checking if user is not loggedIn
    const token = Cookies.get('jwt');
    if (token) {
      setisLoggedIn(true);
      navigate('/Homepage');
    } else {
      navigate('/login');
    }
  }, [navigate, isLoggedIn]);

  const handleSubmit = (event) => {
    event.preventDefault();
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        username,
        password,
      }),
    };
    fetch(process.env.REACT_APP_API_URL + '/account/login', requestOptions)
      .then((x) => x.json())
      .then((user) => {
        //register
        if (user) {
          console.log(user);
          //JWT Token expires in 7 days, and is available to all paths of the site
          Cookies.set('jwt', user.token, { expires: 7, path: '/' });
          navigate('/Homepage');
        }
        //save token to cache
      });
    // Send login request to server here
  };

  return !isLoggedIn ? (
    <></>
  ) : (
    <div className="page page-center">
      <div className="container container-normal py-4">
        <div className="row align-items-center g-4">
          <div className="col-lg">
            <div className="container-tight">
              <div className="text-center mb-4">
                <a href="." className="navbar-brand navbar-brand-autodark">
                  <img
                    src="https://preview.tabler.io/static/logo.svg"
                    height="36"
                    alt=""
                  />
                </a>
              </div>
              <div className="card card-md">
                <div className="card-body">
                  <h2 className="h2 text-center mb-4">Login to your account</h2>
                  <form onSubmit={handleSubmit} autoComplete="off" noValidate>
                    <div className="mb-3">
                      <label className="form-label">Username</label>
                      <input
                        type="text"
                        className="form-control"
                        placeholder="Your username"
                        autoComplete="off"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                      />
                    </div>
                    <div className="mb-2">
                      <label className="form-label">
                        Password
                        <span className="form-label-description">
                          <a href="./forgot-password.html">I forgot password</a>
                        </span>
                      </label>
                      <div className="input-group input-group-flat">
                        <input
                          type="password"
                          className="form-control"
                          placeholder="Your password"
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
                            {/* Download SVG icon from http://tabler-icons.io/i/eye */}
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
                              <path
                                stroke="none"
                                d="M0 0h24v24H0z"
                                fill="none"
                              />
                              <path d="M12 12m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
                              <path d="M22 12c-2.667 4.667 -6 7 -10 7s-7.333 -2.333 -10 -7c2.667 -4.667 6 -7 10 -7s7.333 2.333 10 7" />
                            </svg>
                          </a>
                        </span>
                      </div>
                    </div>
                    {/*  */}
                    <div class="mb-2">
                      <label class="form-check">
                        <input type="checkbox" class="form-check-input" />
                        <span class="form-check-label">
                          Remember me on this device
                        </span>
                      </label>
                    </div>
                    <div class="form-footer">
                      <button type="submit" class="btn btn-primary w-100">
                        Sign in
                      </button>
                    </div>
                  </form>
                </div>
              </div>
              <div class="text-center text-muted mt-3">
                Don't have account yet?{' '}
                <a href="/SignUp" tabindex="-1">
                  Sign up
                </a>
              </div>
            </div>
          </div>
          <div class="col-lg d-none d-lg-block">
            <img
              src="https://preview.tabler.io/static/illustrations/undraw_secure_login_pdn4.svg"
              height="300"
              class="d-block mx-auto"
              alt=""
            />
          </div>
        </div>
      </div>
    </div>
  );
}

export default SignIn;

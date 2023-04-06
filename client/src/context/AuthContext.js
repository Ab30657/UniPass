import { createContext } from 'react';

const AuthContext = createContext({
  isLoggedIn: false,
  user: null,
  login: () => {},
  logout: () => {},
});

export default AuthContext;

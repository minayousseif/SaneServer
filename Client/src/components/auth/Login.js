import React, { useState } from 'react';
import { useDispatch, useSelector } from "react-redux";
import {
  AuthUser,
  ClearUserError
} from '../../store/actions/userActions';
import AlertPanel from '../shared/AlertPanel';
import './Login.scss';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUserShield } from '@fortawesome/free-solid-svg-icons'

const Login = () => {
  document.body.classList.add('bg-gradient-primary');

  const [username, setUsername] = useState(null);
  const [password, setPassword] = useState(null);
  
  const userState = useSelector(state => state.user);
  const dispatch  = useDispatch();
  const hasError  = userState.error != null;

  const handleLoginSubmit = (e) => {
    e.preventDefault();
    dispatch(AuthUser(username, password));
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-sm-9 col-md-7 col-lg-5 mx-auto">
          <div className="login-container">
            <AlertPanel 
              color='danger'
              isOpen={hasError}
              toggle={() => dispatch(ClearUserError())}
            >
              <b>Login failed, The username or password is incorrect.</b>
            </AlertPanel>
            <div className="card card-signin">
              <div className="card-login-icon">
                <FontAwesomeIcon icon={faUserShield} size='4x' />
              </div>
              <div className="card-body">
                <h5 className="card-title text-center">
                    Sign In
                </h5>
                <form className="form-signin" onSubmit={handleLoginSubmit} >
                  <div className="form-label-group">
                    <input
                      type="text"
                      name="username"
                      id="userNameInput"
                      className="form-control"
                      placeholder="User Name"
                      autoComplete='off'
                      onChange={(e) => setUsername(e.target.value)}
                      required
                      autoFocus
                      
                    />
                  </div>

                  <div className="form-label-group">
                    <input
                      type="password"
                      name="password"
                      id="passwordInput"
                      className="form-control"
                      placeholder="Password"
                      onChange={(e) => setPassword(e.target.value)}
                      required
                    />
                  </div>

                  <div className="custom-control custom-checkbox mb-3">
                    <input
                      type="checkbox"
                      className="custom-control-input"
                      id="rememberMeChkBx"
                    />
                    <label
                      className="custom-control-label"
                      htmlFor="rememberMeChkBx"
                    >
                      Remember Me
                    </label>
                  </div>
                  <button
                    className="btn btn-lg btn-primary btn-block text-uppercase"
                    type="submit"
                  >
                    Login
                  </button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
import { userApi } from '../../api/userApi';
import { handleApiErrors } from '../../api/apiHelper';

import {
    AUTH_USER,
    SET_USER_ERROR,
    CLEAR_USER_ERROR
} from '../types/userActionTypes';

import { AuthCookie } from '../../utils/AuthHelper';

const setAuthorizedUser = (user) => ({
    type: AUTH_USER,
    user: user
})

const setUserError = (error) => ({
    type: SET_USER_ERROR,
    error
})

export const ClearUserError = (error) => ({
    type: CLEAR_USER_ERROR
})

export const AuthUser = (username, password) => (dispatch) => {
    userApi.auth(username, password)
    .then((resp) => {
        if (resp.data && resp.data.token) {
            dispatch(setAuthorizedUser(resp.data));
            if (resp.data.token && resp.data.tokenExpiration) {
                AuthCookie.set(resp.data.token, resp.data.tokenExpiration);
                window.location.href = '/';
            } else {
                dispatch(setUserError('Oops! something went wrong, please try again.'));
            }
        }
    })
    .catch((errObj) => handleApiErrors(errObj, (error) => {
        dispatch(setUserError(error));
    }));
};
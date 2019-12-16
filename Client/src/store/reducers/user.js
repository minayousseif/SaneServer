import {
  AUTH_USER,
  SET_USER_ERROR,
  CLEAR_USER_ERROR
} from '../types/userActionTypes';

const userState = {
  error: null
}

const user = (state = userState, action) => {
  switch (action.type) {
    case AUTH_USER:
      return Object.assign({}, state, {
        ...action.user
      });
    case SET_USER_ERROR:
      return Object.assign({}, state, {
        error: action.error
      });
    case CLEAR_USER_ERROR:
        return Object.assign({}, state, {
          error: null
        });
    default:
      return state
  }
}

export default user;
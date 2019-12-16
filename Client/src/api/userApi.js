import axios from 'axios';

export const userApi = {
    auth: (username, password) => (axios.post('/users/auth', {
        username: username,
        password: password
    })),
    get: (user) => {
        /* not implemented */
    }
};
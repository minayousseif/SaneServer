import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { AuthCookie } from '../../utils/AuthHelper';

const AuthorizeRoute = ({ component: Component, ...rest}) => {
    return (
        <Route 
            {...rest}
            render={(props) => {
                if (AuthCookie.get()) {
                    return <Component {...props} />
                } else {
                    return <Redirect to='/login' />
                }
        }} />
    )
};

export default AuthorizeRoute;
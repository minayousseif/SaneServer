import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Login from './components/auth/Login';
import AuthorizeRoute from './components/auth/AuthorizeRoute';

import './App.scss';

export default () => (
    <Layout>
        <AuthorizeRoute exact path='/' component={Home} />
        <Route path='/login' component={Login} />
    </Layout>
);


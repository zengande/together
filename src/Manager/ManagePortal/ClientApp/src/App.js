import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Shared/Layout';
import { Login, Home, FetchData, Counter } from './pages';
import { PrivateRoute} from './components/Authorized';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <div>
                <Layout>
                    <Route exact path='/' component={Home} />
                    <Route path='/counter' component={Counter} />
                    <Route exact path='/login' component={Login} />
                    <PrivateRoute path='/fetch-data' component={FetchData} />
                </Layout>
            </div>
        );
    }
}

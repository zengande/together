import * as React from 'react';
import './App.css';
import { Route } from 'react-router';
import { Authorize } from './components/Authorized';
import { Layout } from './components/Layout';
import { Login, Home } from './pages';

const ProtectedHome = Authorize(Home);

class App extends React.Component<{}> {
    public render() {
        return (
            <Layout>
                <Route exact={true} path={["/", "/home"]} component={ProtectedHome} />
                <Route path="/login" component={Login} />
            </Layout>
        );
    }
}
export default App;
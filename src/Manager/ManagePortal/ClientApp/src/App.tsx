import * as React from 'react';
import './App.css';
import { Route, Switch, Redirect } from 'react-router';
import { Authorize } from './components/Authorized';
import BasicLayout from './layouts/BasicLayout';
import { Login, Home, Exception404 } from './pages';

const ProtectedHome = Authorize(Home);

class App extends React.Component<{}> {
    public render() {

        const layoutRoutes = (
            <BasicLayout>
                <Switch>
                    <Route exact={true} path={["/", "/home"]} component={ProtectedHome} />
                </Switch>
            </BasicLayout>
        );

        const routes = (
            <Switch>
                <Route exact={true} path="/" render={props => layoutRoutes} />
                <Route path="/login" component={Login} />
                <Route path="/404" component={Exception404} />
                <Redirect from='*' to='/404' />
            </Switch>
        )

        return routes;
    }
}
export default App;
import * as React from 'react';
import './App.css';
import { Route, Switch, Redirect } from 'react-router';
import { Authorize } from './components/Authorized';
import { Login, Home, Exception404, Center } from './pages';
import UserManagement from './pages/usermanagement/UserManagement';
import ManagementLayout from './layouts/ManagementLayout';

class App extends React.Component<{}> {
    public render() {

        const managementRoutes = ({ match }: { match: any }) => {
            return (
                <ManagementLayout>
                    <Switch>
                        <Route path={`${match.url}/user`} component={Authorize(UserManagement)} />
                        <Route path={`${match.url}/activity`} component={Authorize(() => (<div>activity</div>))} />
                        <Redirect from='*' to='/404' />
                    </Switch>
                </ManagementLayout>
            )
        };

        const accountRoutes = ({ match }: { match: any }) => (
            <Switch>
                <Route path={`${match.url}/center`} component={Authorize(Center)} />
                <Route path={`${match.url}/login`} component={Login} />
                <Redirect from='*' to='/account/login' />
            </Switch>
        );

        const routes = (
            <Switch>
                <Route exact={true} path="/" component={Authorize(Home)} />
                <Route path="/management" component={managementRoutes} />
                <Route path="/account" component={accountRoutes} />
                <Route path="/404" component={Exception404} />
                <Redirect from='*' to='/404' />
            </Switch>
        )

        return routes;
    }
}
export default App;
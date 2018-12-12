import * as React from 'react';
import './App.css';
import { Route, Switch, Redirect } from 'react-router';
import { Authorize } from './components/Authorized';
import { Login, Home, Exception404 } from './pages';
import UserManagement from './pages/usermanagement/UserManagement';

class App extends React.Component<{}> {
    public render() {

        const managementRoutes = ({ match }: { match: any }) => {
            return (
                <Switch>
                    <Route path={`${match.url}/user`} component={Authorize(UserManagement)} />
                    <Route path={`${match.url}/activity`} component={Authorize(() => (<div>activity</div>))} />
                    <Redirect from='*' to='/404' />
                </Switch>
            )
        };

        const routes = (
            <Switch>
                <Route exact={true} path="/" component={Authorize(Home)} />
                <Route path="/management" component={managementRoutes} />
                <Route path="/login" component={Login} />
                <Route path="/404" component={Exception404} />
                <Redirect from='*' to='/404' />
            </Switch>
        )

        return routes;
    }
}
export default App;
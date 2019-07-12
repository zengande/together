import * as React from 'react';
import { Authorize } from './components/Authorized/Authorize';
import { Route, Switch, Redirect, BrowserRouter } from 'react-router-dom';
import { Exception404 } from './pages';
import BasicLayout from './layouts/BasicLayout';
import Loadable from 'react-loadable';
import LoadingSpinner from './components/LoadingSpinner/index';

export const asyncComponent = (loader, loading = <LoadingSpinner />) => {
    return Loadable({
        loader: loader,
        loading: () => loading
    });
}

export default class RouterConfig extends React.Component {
    render() {
        const BasicRoute = ({ component: Component, ...rest }) => {
            return (
                <Route {...rest} render={matchProps => (
                    <BasicLayout showFooter={{ ...rest }.showFooter}>
                        <Component {...matchProps} />
                    </BasicLayout>
                )} />
            )
        };

        const Home = Loadable({ loader: () => import('./pages/home/Home'), loading: () => <LoadingSpinner /> });
        const Analysis = asyncComponent(() => import('./pages/analysis/Analysis'));
        const User = asyncComponent(() => import('./pages/management/User'));
        const Center = asyncComponent(() => import('./pages/account/center/index'));
        const Login = asyncComponent(() => import('./pages/account/login/index'));

        return (
            <BrowserRouter >
                <React.Suspense fallback={<LoadingSpinner />}>
                    <Switch>
                        <BasicRoute exact={true} path="/" component={Authorize(Home)} />
                        <BasicRoute path="/analysis" component={Authorize(Analysis)} />
                        <BasicRoute showFooter={false} path="/management/user" component={Authorize(User)} />
                        <BasicRoute showFooter={false} path="/management/activity" component={Authorize(() => (<div>activity</div>))} />
                        <BasicRoute path="/account/center" component={Authorize(Center)} />

                        <Route path="/account/login" component={Login} />
                        <Route path="/404" component={Exception404} />
                        <Redirect from='*' to='/404' />
                    </Switch>
                </React.Suspense>
            </BrowserRouter >
        )
    }
}
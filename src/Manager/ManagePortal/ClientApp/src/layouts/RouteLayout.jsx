import * as React from 'react';
import BasicLayout from './BasicLayout';
import { Route } from 'react-router-dom';

export const BasicRoute = ({ component: Component, ...rest }) => {
    return (
        <Route {...rest} render={matchProps => (
            <BasicLayout>
                <Component {...matchProps} />
            </BasicLayout>
        )} />
    )
};
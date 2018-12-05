import React from 'react';
import {
    Route,
    Redirect
} from "react-router-dom";
import { securityService } from '../../services/security.service'

class PrivateRoute extends React.Component {
    render() {
        const { component: Component, ...rest } = this.props;
        return (
            <Route {...rest}
                render={props =>
                    securityService.IsAuthorized ? (
                        <Component {...props} />
                    ) : (
                            <Redirect to={{ pathname: "/login", state: { from: props.location } }} />
                        )
                }
            />);
    }
}

export default PrivateRoute;
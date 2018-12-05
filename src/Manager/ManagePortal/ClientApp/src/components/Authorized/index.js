import React from 'react';
import PropTypes from 'prop-types';
import { securityService } from '../../services/security.service';
import PrivateRoute from './PrivateRoute';

class Authorized extends React.Component {

    render() {
        const { children, roles, exception } = this.props;
        if (securityService.IsAuthorized) {
            if (roles &&
                roles.length > 0) {
                // todo : check premission
            }
            return children
        } else {
            return exception;
        }
    }
}

Authorized.defaultProps = {
    exception: (<h1> 你没有查看的权限</h1>),
    roles: []
};

Authorized.propTypes = {
    children: PropTypes.node,
    exception: PropTypes.node,
    roles: PropTypes.arrayOf(PropTypes.string)
};

export {
    PrivateRoute,
    Authorized
}
import React from 'react';
import { securityService } from '../services/security.service'

class Login extends React.Component {
    handleClick() {
        securityService.Authorize();
    }

    render() {
        return (<button onClick={this.handleClick}>点击登录</button>)
    }
}

export default Login;
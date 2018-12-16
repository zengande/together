import * as React from 'react';
import LoginForm from 'src/components/Login/LoginForm';
import './index.css';

class Login extends React.Component<any> {
    public render() {
        return (
            <div className="login-container">
                <h1 style={{ textAlign: 'center' }}>LOGIN</h1>
                <LoginForm {...this.props} />
            </div>
        );
    }
}

export default Login;
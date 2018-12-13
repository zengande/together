import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from 'src/store/userStore';
import { securityService } from 'src/services/security.service';

class Login extends React.Component<any> {
    constructor(props: any) {
        super(props);
        this.login = this.login.bind(this);
    }

    public componentWillMount() {
        const { logout } = this.props;
        logout();
        securityService.ResetAuthorizationData();
    }

    public render() {
        return (
            <button onClick={this.login}>登录</button>
        );
    }

    private login() {

        const { login } = this.props;
        let result = securityService.Authorize('zengande', 'password');
        if (result) {
            const userInfo = securityService.GetUserInfo();
            login(userInfo);
            const redirect = this.getRedirect();
            this.props.history.push(redirect)
        }
        // todo : login failed!
    }

    private getRedirect(): string {
        const returnUrl = this.getQueryString('redirect');
        if (returnUrl && returnUrl !== '') {
            return returnUrl;
        }
        return '/'
    }


    private getQueryString(name: string): string | undefined {
        const reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        let r = this.props.location.search.substr(1).match(reg);
        if (r != null) {
            return unescape(r[2]);
        } 
        return undefined;
    }
}

export default connect(
    null,
    (dispatch: any) => bindActionCreators(actionCreators, dispatch)
)(Login)
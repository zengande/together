import * as React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import './index.css'

class Header extends React.Component<any> {
    public render() {

        const { user } = this.props;
        let loginBlock = null;
        if (user.isAuthenticated) {
            loginBlock = <div>
                <h1>{user.userInfo.nickname}</h1>
            </div>
        } else {
            loginBlock = <div>
                <Link to={'/login'}>登录</Link>
            </div>
        }
        return (
            <div className="header">
                {loginBlock}
            </div>
        )
    }
}

export default connect(
    (state: any) => ({ user: state.user })
)(Header)
import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from 'src/types';
import { Dropdown, Avatar, Spin, Menu } from 'antd';
import { Link, withRouter } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from 'src/store/userStore';

export class RightContent extends React.Component<any> {
    public shouldComponentUpdate(nextProps: any) {
        if (this.props.identity !== nextProps.props) {
            return true;
        }
        return false;
    }

    public render() {
        const { identity } = this.props;
        const menu = (
            <Menu>
                <Menu.Item><Link to="/account/center">我的资料</Link></Menu.Item>
                <Menu.Item><a href="javascript:;" onClick={() => { this.logout() }}>退出登录</a></Menu.Item>
            </Menu >
        );
        return (
            <div className="header-right">
                {
                    identity.isAuthenticated ? (
                        <Dropdown overlay={menu}>
                            <span>
                                <Avatar
                                    size="small"
                                    alt="avatar"
                                />
                                <span className="name">{identity.userInfo.nickname}</span>
                            </span>
                        </Dropdown>
                    ) : (<Spin size="small" style={{ marginLeft: 8, marginRight: 8 }} />)
                }
            </div>
        );
    }

    private logout() {
        const { logout, history } = this.props;
        logout();
        history.push('/account/login')
    }

}

export default connect(
    (state: IState) => ({
        identity: state.identity
    }),
    (dispatch: any) => bindActionCreators(actionCreators, dispatch)
)(withRouter(RightContent));
import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from 'src/types';
import { Dropdown, Avatar, Spin, Menu } from 'antd';
import { Link } from 'react-router-dom';
class LeftContent extends React.Component<any> {

    public shouldComponentUpdate(nextProps: any) {
        console.log(nextProps);
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
                <Menu.Item><Link to="">退出</Link></Menu.Item>
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
}

export default connect(
    (state: IState) => ({
        identity: state.identity
    })
)(LeftContent);
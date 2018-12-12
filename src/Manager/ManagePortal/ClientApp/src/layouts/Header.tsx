import * as React from 'react';
import { connect } from 'react-redux';
import { Icon, Avatar, Spin, Menu } from 'antd';
import './Header.css';
import HeaderDropdown from '../components/HeaderDropdown/index';
import { Link } from 'react-router-dom';

class Header extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.toggle = this.toggle.bind(this);
    }

    public render() {
        const { collapsed, identity } = this.props;

        const menu = (
            <Menu>
                <Menu.Item><Link to="">我的资料</Link></Menu.Item>
                <Menu.Item><Link to="">退出</Link></Menu.Item>
            </Menu >
        );

        return (
            <nav className="fix-header">
                <div className="header-left">
                    <Icon type={collapsed ? 'menu-unfold' : 'menu-fold'} onClick={this.toggle} />
                    <Link to="/">TOGETHER</Link></div>
                <div className="header-right">
                    {identity.isAuthenticated ? (
                        <HeaderDropdown overlay={menu}>
                            <span>
                                <Avatar
                                    size="small"
                                    alt="avatar"
                                />
                                <span className="name">{identity.userInfo.nickname}</span>
                            </span>
                        </HeaderDropdown>
                    ) : (<Spin size="small" style={{ marginLeft: 8, marginRight: 8 }} />)}
                </div>
            </nav>
        )
    }

    private triggerResizeEvent() {
        const event = document.createEvent('HTMLEvents');
        event.initEvent('resize', true, false);
        window.dispatchEvent(event);
    }

    private toggle() {
        const { collapsed, onCollapse } = this.props;
        onCollapse(!collapsed);
        this.triggerResizeEvent();
    }
}

export default connect(
    (state: any) => {
        return { identity: state.identity }
    }
)(Header);
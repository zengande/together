import * as React from 'react';
import { connect } from 'react-redux';
import { Layout, Icon, Avatar, Spin, Menu } from 'antd';
import './Header.css';
import HeaderDropdown from '../components/HeaderDropdown/index';

class Header extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.toggle = this.toggle.bind(this);
    }

    public render() {
        const { collapsed, identity } = this.props;

        const menu = (
            <Menu>
                <Menu.Item>1st menu item</Menu.Item>
                <Menu.Item>2nd menu item</Menu.Item>
                <Menu.Item>5d menu item</Menu.Item>
                <Menu.Item>6th menu item</Menu.Item>
            </Menu >
        );

        return (
            <Layout.Header style={{ background: '#fff', padding: 0 }}>
                <span onClick={this.toggle}>
                    <Icon type={collapsed ? 'menu-unfold' : 'menu-fold'} />
                </span>
                <div className="right">
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
            </Layout.Header>
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
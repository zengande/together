import * as React from 'react';
import DocumentTitle from 'react-document-title';
import { Layout, Menu, Icon } from 'antd';
import Header from './Header';
import Footer from './Footer';

const { Content, Sider } = Layout;

class BasicLayout extends React.PureComponent<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            collapsed: false
        }
    }


    public render() {
        const { children } = this.props;
        const layout = (
            <Layout style={{ minHeight: "100vh" }}>
                <Sider
                    trigger={null}
                    collapsible={true}
                    collapsed={this.state.collapsed}
                >
                    <div className="logo" />
                    <Menu theme="dark" mode="inline" defaultSelectedKeys={['1']}>
                        <Menu.Item key="1">
                            <Icon type="user" />
                            <span>nav 1</span>
                        </Menu.Item>
                        <Menu.Item key="2">
                            <Icon type="video-camera" />
                            <span>nav 2</span>
                        </Menu.Item>
                        <Menu.Item key="3">
                            <Icon type="upload" />
                            <span>nav 3</span>
                        </Menu.Item>
                    </Menu>
                </Sider>
                <Layout>
                    <Header collapsed={this.state.collapsed}
                        onCollapse={(isCollapsed: boolean) => {
                            this.setState({ collapsed: isCollapsed })
                        }} />
                    <Content>
                        {children}
                    </Content>
                    <Footer />
                </Layout>
            </Layout>
        );

        return (
            <DocumentTitle title={"together"}>
                <div> {layout}</div>
            </DocumentTitle>
        );
    }
}

export default BasicLayout;
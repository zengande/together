import * as React from 'react';
import Header from './Header';
import { Layout } from 'antd';
import Footer from './Footer';
import BasicLayout from './BasicLayout';
import './MainLayout.css';

const { Content } = Layout;

export default class MainLayout extends React.PureComponent<any, any>{
    constructor(props: any) {
        super(props);
        this.state = {
            collapsed: false
        }
    }

    public componentWillMount() {
        this.setState({ loading: true });
    }

    public componentDidMount() {
        setTimeout(() => {
            this.setState({ loading: false })
        }, 1000);

    }

    public render() {
        const { children } = this.props;

        const layout = (
            <div className="main-container">
                <Header collapsed={this.state.collapsed}
                    onCollapse={(isCollapsed: boolean) => {
                        this.setState({ collapsed: isCollapsed })
                    }} />
                <Content className="main-content">
                    {children}
                </Content>
                <Footer />
            </div >
        );

        return (
            <BasicLayout>
                {layout}
            </BasicLayout>
        )
    }
}
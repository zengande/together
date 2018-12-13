import * as React from 'react';
import { Layout } from 'antd';
import Footer from './Footer';
import BasicLayout from './BasicLayout';
import './MainLayout.css';

const { Content } = Layout;

export default class MainLayout extends React.PureComponent<any, any>{

    public render() {
        const { children } = this.props;

        const content = (
            <React.Fragment>
                <Content className="main-content">
                    {children}
                </Content>
                <Footer />
            </React.Fragment>
        );

        return (
            <BasicLayout>
                {content}
            </BasicLayout>
        )
    }
}
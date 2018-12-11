import * as React from 'react';
import DocumentTitle from 'react-document-title';
import { Layout } from 'antd';
import Header from './Header';
import Footer from './Footer';
import LoadingSpinner from '../components/LoadingSpinner/index';
import './BasicLayout.css';

const { Content } = Layout;

class BasicLayout extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            collapsed: false,
            loading: true
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

        if (this.state.loading) {
            return (<LoadingSpinner />)
        } else {
            return (
                <DocumentTitle title={"together"}>
                    {layout}
                </DocumentTitle>
            )
        }
    }
}

export default BasicLayout;
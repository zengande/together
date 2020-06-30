import React from 'react';
import GlobalHeader from '@/components/global-header';
import { Layout } from 'antd'
import { connect, Loading, ConnectProps } from 'umi';
import { AuthModelState } from '@/models/auth';
import styles from './index.less'

const { Content, Footer } = Layout;

interface BasicLayoutProps extends ConnectProps {
    isAuthenticated: boolean
}

class BasicLayout extends React.PureComponent<BasicLayoutProps> {
    componentDidMount() {
    }

    render() {
        const { children } = this.props;
        return (
            <Layout className={styles.layout}>
                <GlobalHeader {...this.props} />
                <Content className={styles.content}>
                    {children}
                </Content>
                <Footer className={styles.footer}>TOGETHER 2020</Footer>
            </Layout>
        )
    }
}

export default connect(({ loading, auth }: { loading: Loading, auth: AuthModelState }) => ({
    isAuthenticated: auth.isAuthenticated
}))(BasicLayout);
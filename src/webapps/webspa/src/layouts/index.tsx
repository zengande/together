import React from 'react';
import GlobalHeader from '@/components/global-header';
import { Layout } from 'antd'
import { connect, Loading, ConnectProps, GlobalModelState } from 'umi';
import { AuthModelState } from '@/models/auth';
import styles from './index.less'
import UserLocation from '@/@types/user/location';

const { Content, Footer } = Layout;

interface BasicLayoutProps extends ConnectProps {
    isAuthenticated: boolean,
    userLocation?: UserLocation
}

class BasicLayout extends React.PureComponent<BasicLayoutProps> {
    componentDidMount() {
        this.props.dispatch!({ type: 'global/fetchUserLocation' })
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

export default connect(({ loading, auth, global }: { loading: Loading, global: GlobalModelState, auth: AuthModelState }) => ({
    isAuthenticated: auth.isAuthenticated,
    ...global
}))(BasicLayout);
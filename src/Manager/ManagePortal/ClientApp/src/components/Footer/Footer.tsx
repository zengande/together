import * as React from 'react';
import { Layout } from 'antd';

export default class Footer extends React.PureComponent {
    public render() {
        return (
            <Layout.Footer style={{ textAlign: "center" }}>
                TOGETHER.CN
            </Layout.Footer>
        )
    }
}
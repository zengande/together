import * as React from 'react';
import './index.css';
import { Spin } from 'antd';

export default class LoadingSpinner extends React.PureComponent {
    public render() {
        return (
            <div className="loader-wrapper">
                <Spin size="large" />
            </div>
        )
    }
}
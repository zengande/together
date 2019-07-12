import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from '../../../types/StateTypes';

class Center extends React.Component<any> {
    public render() {
        const { identity: { userInfo } } = this.props;

        return (<h1>{userInfo.nickname}个人中心</h1>)
    }
}

export default connect(
    (state: IState) => ({
        ...state
    })
)(Center);
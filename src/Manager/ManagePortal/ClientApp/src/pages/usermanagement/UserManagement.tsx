import * as React from 'react';
import BlankLayout from 'src/layouts/BlankLayout';
import { connect } from 'react-redux';
import { IState } from 'src/types';

class UserManagement extends React.Component<any> {
    public render() {
        const { menus } = this.props;
        return (<BlankLayout><h1>{JSON.stringify(menus)}</h1></BlankLayout>)
    }
}

export default connect(
    (state: IState) => ({ ...state.menu })
)(UserManagement);
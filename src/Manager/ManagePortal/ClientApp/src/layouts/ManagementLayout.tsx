import * as React from 'react';
import * as PropTypes from 'prop-types';
import BasicLayout from './BasicLayout';
import './ManagementLayout.css';

export default class ManagementLayout extends React.Component {
    public static propTypes = {
        children: PropTypes.node.isRequired
    }

    public render() {
        return (
            <BasicLayout>
                <div className="manage-container">
                    <div className="manage-menus">left</div>
                    <div className="manage-content">
                        {this.props.children}
                    </div>
                </div>
            </BasicLayout>
        )
    }
}
import * as React from 'react';
import { Dropdown } from 'antd';
import classNames from 'classnames';


export default class HeaderDropdown extends React.PureComponent<any> {
    public render() {
        const { overlayClassName, overlay, ...props } = this.props;
        return (
            <Dropdown overlay={overlay} className={classNames("container", overlayClassName)} {...props} />
        );
    }
}
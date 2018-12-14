import * as React from 'react';
import './Header.css';
import RightContent from './RightContent';
import LeftContent from './LeftContent';

export default class Header extends React.Component<any, any> {

    public render() {
        const { collapsed, onCollapse } = this.props;

        return (
            <nav className="fix-header">
                <LeftContent />
                <RightContent collapsed={collapsed}
                    onCollapse={onCollapse} />
            </nav>
        )
    }
}
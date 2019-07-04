import { IMenu } from '../../types/IMenu';
import * as React from 'react';
import { Icon } from 'antd';

export default class DrawerMenuItem extends React.PureComponent<{ menu: IMenu }> {
    public render() {
        const { menu } = this.props;
        return (
            <div className="drawer-menu-item">
                <a href={menu.link}>
                    <Icon type={menu.icon} />
                    <span>{menu.text}</span>
                </a>
                <div className="right-blank">
                    <a title="新页面打开" target="_blank" href={menu.link}><Icon type="ellipsis" /></a>
                </div>
            </div>
        )
    }
}
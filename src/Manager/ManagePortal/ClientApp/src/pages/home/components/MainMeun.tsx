import * as React from 'react';
import './MainMenu.css';
import { Link } from 'react-router-dom';
import { IMenu } from '../types/IMenu';
import { Icon } from 'antd';

class MenuItem extends React.PureComponent<{ menu: IMenu }>{
    public render() {
        const { menu } = this.props;
        return (
            <Link to={menu.Link} className="menu-item" target="_blank">
                <Icon className="menu-item-icon" type={menu.Icon} />
                <div>
                    <div className="menu-item-text">{menu.Text}</div>
                </div>
            </Link>
        );
    }
}

export default class MainMenu extends React.Component<{ menus: IMenu[] }> {
    public render() {
        const { menus } = this.props;
        return (
            <div className="menus-container">
                <h2 className="block-heading">菜单</h2>
                <div className="menus">
                    {
                        (menus && menus.length > 0) ?
                            menus.map((value: IMenu, index: number) => {
                                return (<MenuItem key={index} menu={value} />);
                            }) : (
                                <div className="no-result"> 没有应用 </div>
                            )
                    }
                </div>
            </div>
        )

    }
}


import * as React from 'react';
import './MainMenu.css';
import { Link } from 'react-router-dom';
import { Icon } from 'antd';
import { IMenu } from 'src/types/IMenu';
import { IState } from 'src/types';
import { connect } from 'react-redux';

class MenuItem extends React.PureComponent<{ menu: IMenu }>{
    public render() {
        const { menu } = this.props;
        return (
            <Link to={menu.link} className="menu-item">
                <Icon className="menu-item-icon" type={menu.icon} />
                <div>
                    <div className="menu-item-text">{menu.text}</div>
                </div>
            </Link>
        );
    }
}

class MainMenu extends React.PureComponent<any> {
    public render() {
        const { menus } = this.props;
        return (
            <div className="menus-container">
                <h2 className="block-heading">主菜单</h2>
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

export default connect(
    (state: IState) => ({ menus: state.menu.menus })
)(MainMenu);
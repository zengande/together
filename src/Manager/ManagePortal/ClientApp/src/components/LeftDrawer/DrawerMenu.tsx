import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from 'src/types';
import { IMenu } from 'src/types/IMenu';
import DrawerMenuItem from './DrawerMenuItem';
import { Icon } from 'antd';

class DrawerMenu extends React.Component<any> {
    public render() {
        const { menus } = this.props;

        return (
            <div>
                <h2>主菜单</h2>
                <div className="drawer-menus">
                    {
                        menus.map((value: IMenu, index: number) => {
                            return (<DrawerMenuItem key={index} menu={value} />);
                        })
                    }
                    <div className="back-home">
                        <a href='/'>
                            <Icon type="arrow-left" />
                            <span>返回主页</span>
                        </a>
                    </div>
                </div>
            </div>
        )
    }
}

export default connect(
    (state: IState) => ({ ...state.menu })
)(DrawerMenu);
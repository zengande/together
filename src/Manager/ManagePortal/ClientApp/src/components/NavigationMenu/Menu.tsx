import * as React from 'react';
import { Link, withRouter, RouteComponentProps } from 'react-router-dom';
import { IMenu } from '../../types/IMenu';
import { Menu } from 'antd';

type PropsType = RouteComponentProps<any> & {
    menus: IMenu[]
}

class NavigationMenu extends React.Component<PropsType> {

    public render() {
        const { menus } = this.props;
        return (
            <Menu
                style={{ width: "100%", height: '100%' }}
                defaultSelectedKeys={['#']}
                selectedKeys={this.currentKeys()}
            >
                {
                    menus.map((value: IMenu, index: number) => {
                        console.log(value.link);
                        return (<Menu.Item key={value.link}><Link to={value.link}>{value.text}</Link></Menu.Item>)
                    })
                }
            </Menu>
        )
    }

    private currentKeys(): string[] {
        const { location: { hash } } = this.props;
        if (hash === '') {
            return ["#"];
        }
        return [hash];
    }
}

export const NavigationMenuWithRouter = withRouter(NavigationMenu)

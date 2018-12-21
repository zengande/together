import * as React from 'react';
import { NavigationMenuWithRouter } from 'src/components/NavigationMenu/Menu';
import { INavigationMenu } from 'src/types/INavigationMenu';
import UserList from '../user/UserList';
import RoleList from '../user/RoleList';

export default class User extends React.Component<any> {
    public render() {
        const menus: INavigationMenu[] = [
            { text: '用户管理', link: '#', icon: '', component: UserList },
            { text: '角色管理', link: '#role', icon: '', component: RoleList },
        ];

        let Component: any = UserList;
        const { location: { hash } } = this.props;
        if (hash) {
            let result = menus.filter((value) => value.link === hash);
            if (result.length && result.length > 0) {
                Component = result[0].component;
            }
        }

        return (
            <div className="management-container">
                <div className="nav-menus-content">
                    <NavigationMenuWithRouter menus={menus} />
                </div>
                <div className="management-content">
                    <Component />
                </div>
            </div>
        )
    }
}
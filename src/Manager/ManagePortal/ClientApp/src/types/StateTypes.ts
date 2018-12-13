import { IUserInfo } from './IUserInfo';
import { IMenu } from './IMenu';

export interface IIdentityState {
    isAuthenticated: boolean;
    userInfo: IUserInfo
}

export interface IMenuState {
    menus: IMenu[];
    drawerVisible: boolean
}

export interface IState {
    identity: IIdentityState,
    menu: IMenuState
}
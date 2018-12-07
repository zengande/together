import { IUserInfo } from './IUserInfo';
export interface IIdentityState {
    isAuthenticated: boolean;
    userInfo:IUserInfo
}

export interface IState {
    identity: IIdentityState
}
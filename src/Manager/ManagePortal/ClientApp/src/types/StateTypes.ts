import { IUserInfo } from './IUserInfo';
export interface IUserState {
    isAuthenticated: boolean;
    userInfo:IUserInfo
}

export interface IState {
    user: IUserState
}
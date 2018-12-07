import { Reducer } from 'redux';
import { IIdentityState, AT } from '../types';
import { IUserInfo } from 'src/types/IUserInfo';
import { storeService } from '../services/storage.service'

const getInitialState = () => {
    const defaultUser: IIdentityState = {
        isAuthenticated: false,
        userInfo: {
            id: null,
            username: null,
            nickname: null
        }
    }
    const userInfo = storeService.retrieve("userData", null);
    const isAuthenticated: boolean = storeService.retrieve('IsAuthorized', false);
    const result = {
        ...defaultUser,
        isAuthenticated: (isAuthenticated && userInfo != null),
        userInfo: { ...userInfo }
    };
    return result;
}

const initialState: IIdentityState = getInitialState();

export const actionCreators = {
    login: (userInfo: IUserInfo) => ({
        type: AT.USER_LOGIN,
        userInfo
    }),
    logout: () => ({
        type: AT.USER_LOGOUT
    })
}

export const reducer: Reducer<IIdentityState> = (state = initialState, action) => {
    if (action.type === AT.USER_LOGIN) {
        return {
            ...state,
            isAuthenticated: true,
            userInfo: { ...action.userInfo }
        }
    } else if (action.type === AT.USER_LOGOUT) {
        return { ...initialState }
    }
    return state;
}
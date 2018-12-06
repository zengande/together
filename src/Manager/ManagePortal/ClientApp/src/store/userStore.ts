import { Reducer } from 'redux';
import { IUserState, AT } from '../types';

const initialState: IUserState = {
    isAuthenticated: false,
    id: null,
    username: null,
    nickname: null
}

export const actionCreators = {

}

export const reducer: Reducer<IUserState> = (state = initialState, action) => {
    if (action.type === AT.USER_LOGIN) {
        return {
            ...state,
            isAuthenticated: true,
            username: action.username,
            nickname: action.nickname,
            id: action.id
        }
    } else if (action.type === AT.USER_LOGOUT) {
        return { ...initialState }
    }
    return state;
}
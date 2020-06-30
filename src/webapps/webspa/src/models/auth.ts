import { Reducer, Effect } from 'umi';
import AuthService from '@/services/auth.service'

export interface AuthModelState {
    isAuthenticated: boolean;
}

export interface AuthModelType {
    namespace: 'auth';
    state: AuthModelState;
    effects: {
    };
    reducers: {
        save: Reducer<AuthModelState>;
    };
}

const Model: AuthModelType = {
    namespace: 'auth',
    state: {
        isAuthenticated: AuthService.isAuthenticated()
    },
    effects: {
    },
    reducers: {
        save(state, action) {
            return {
                ...state,
                ...action.payload,
            };
        },
    }
}

export default Model;
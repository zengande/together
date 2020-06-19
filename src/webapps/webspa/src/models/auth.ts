import { Reducer, Effect } from 'umi';
import AuthService from '@/services/auth.service'

export interface AuthModelState {
    isAuthenticated: boolean;
}

export interface AuthModelType {
    namespace: 'auth';
    state: AuthModelState;
    effects: {
        init: Effect
    };
    reducers: {
        save: Reducer<AuthModelState>;
    };
}

const Model: AuthModelType = {
    namespace: 'auth',
    state: {
        isAuthenticated: false
    },
    effects: {
        *init(_, { call, put }) {
            const isAuthenticated = yield call(AuthService.isAuthenticated);
            yield put({
                type: 'save',
                payload: {
                    isAuthenticated
                }
            })
        }
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
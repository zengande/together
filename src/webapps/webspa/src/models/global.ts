import { Reducer, Effect } from 'umi';
import UserLocation from '@/@types/user/location';
import locationService from '@/services/location.service'

export interface GlobalModelState {
    userLocation?: UserLocation
}

export interface GlobalModelType {
    namespace: "global",
    state: GlobalModelState,
    effects: {
        fetchUserLocation: Effect,
        updateUserLocation: Effect,
    },
    reducers: {
        save: Reducer<GlobalModelState>;
    }
}

export default {
    namespace: "global",
    state: {},
    effects: {
        *fetchUserLocation({ }, { put, call }) {
            const userLocation = yield call(locationService.getUserLocation);
            yield put({
                type: 'save',
                payload: {
                    userLocation
                }
            })
        },
        *updateUserLocation({ payload }, { put }) {
            yield put({
                type: 'save',
                payload: {
                    userLocation: payload
                }
            })
        }
    },
    reducers: {
        save(state, { payload }) {
            return {
                ...state,
                ...payload
            }
        }
    }
} as GlobalModelType;
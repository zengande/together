import { Effect, Reducer, request } from 'umi';
import { CascaderOptionType } from 'antd/lib/cascader';
import ActivityCatalog from '@/@types/activity/catalog';

export interface CreateModelState {
    catalogs?: ActivityCatalog[]
}

export interface CreateModelType {
    namespace: 'activitycreate';
    state: CreateModelState;
    effects: {
        fetchCatalogs: Effect
    };
    reducers: {
        save: Reducer<CreateModelState>;
    };
}

const Model: CreateModelType = {
    namespace: 'activitycreate',
    state: {
        catalogs: []
    },
    effects: {
        *fetchCatalogs({ payload }, { call, put, select }) {
            const catalogs = yield call(request, '[ApiBaseAddress]/api/Catalogs')
            console.log(catalogs);
            yield put({ type: 'save', payload: { catalogs } })
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
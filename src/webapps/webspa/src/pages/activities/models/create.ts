import { Effect, Reducer, request } from 'umi';
import { CascaderOptionType } from 'antd/lib/cascader';
import ActivityCatalog from '@/@types/activity/catalog';
import ActivityService from "@/services/activity.service";

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
            const catalogs = yield call(ActivityService.fetchCatalogs)
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
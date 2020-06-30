import { Effect, Reducer, request, history } from 'umi';
import { CascaderOptionType } from 'antd/lib/cascader';
import ActivityCategory from '@/@types/activity/category';
import ActivityService from "@/services/activity.service";

export interface CreateModelState {
    categories?: ActivityCategory[]
}

export interface CreateModelType {
    namespace: 'activitycreate';
    state: CreateModelState;
    effects: {
        post: Effect;
        fetchCategories: Effect;
    };
    reducers: {
        save: Reducer<CreateModelState>;
    };
}

const Model: CreateModelType = {
    namespace: 'activitycreate',
    state: {
        categories: []
    },
    effects: {
        *post({ payload }, { call, put }) {
            const activityId = yield call(ActivityService.create, payload);
            if (activityId && activityId > 0) {
                history.push(`/activities/${activityId}`);
            }
        },
        *fetchCategories({ payload }, { call, put, select }) {
            const categories = yield call(ActivityService.getCategories)
            yield put({ type: 'save', payload: { categories } })
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
import { Reducer, Effect } from 'umi';
import ActivityService from '@/services/activity.service';
import Activity, { Participant } from '@/@types/activity/activity';

export interface ActivityModelState {
    activity?: Activity;
    participants?: Participant[];
}

export interface ActivityModelType {
    namespace: 'activity'
    state: ActivityModelState;
    effects: {
        fetch: Effect;
        fetchParticipants: Effect;
        join: Effect;
        collect: Effect;
    }
    reducers: {
        save: Reducer<ActivityModelType>;
    };
}

const Model: ActivityModelType = {
    namespace: 'activity',
    state: {
    },
    effects: {
        *fetch({ payload }, { call, put }) {
            try {
                const activity = yield call(ActivityService.getActivity, payload);
                yield put({
                    type: 'save',
                    payload: {
                        activity
                    }
                })
            } catch{
                console.log('error');
            }
        },
        *fetchParticipants({ payload }, { call, put }) {
            try {
                const participants = yield call(ActivityService.getParticipants, payload);
                yield put({
                    type: 'save',
                    payload: {
                        participants
                    }
                })
            } catch{
                console.log('error');
            }
        },
        *join({ payload }, { call, put, select }) {
            yield call(ActivityService.join, payload);
            const activity = yield select(({ activity }: { activity: ActivityModelState }) => (activity.activity));
            yield put({
                type: 'save',
                payload: {
                    activity: {
                        ...activity,
                        isJoined: true
                    }
                }
            })
        },
        *collect({ payload }, { call, put, select }) {
            yield call(ActivityService.collect, payload);
            const activity = yield select(({ activity }: { activity: ActivityModelState }) => (activity.activity));
            yield put({
                type: 'save',
                payload: {
                    activity: {
                        ...activity,
                        isCollected: true
                    }
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
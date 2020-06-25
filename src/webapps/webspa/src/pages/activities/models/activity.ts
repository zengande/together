import { Reducer, Effect } from 'umi';
import ActivityService from '@/services/activity.service';
import Activity, { Atteandee } from '@/@types/activity/activity';

export interface ActivityModelState {
    activity?: Activity;
    attendees?: Atteandee[];
}

export interface ActivityModelType {
    namespace: 'activity'
    state: ActivityModelState;
    effects: {
        fetch: Effect;
        fetchAttendees: Effect;
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
        *fetchAttendees({ payload }, { call, put }) {
            try {
                const attendees = yield call(ActivityService.getAttendees, payload);
                yield put({
                    type: 'save',
                    payload: {
                        attendees
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
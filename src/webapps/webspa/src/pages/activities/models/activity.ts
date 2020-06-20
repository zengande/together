import { Reducer, Effect } from 'umi';
import ActivityService from '@/services/activity.service';
import Activity, { Participant } from '@/@types/activity/activity';

export interface ActivityModelState {
    activity?: Activity;
    participants?: Participant[]
}

export interface ActivityModelType {
    namespace: 'activity'
    state: ActivityModelState;
    effects: {
        fetch: Effect;
        fetchParticipants: Effect;
    }
    reducers: {
        save: Reducer<ActivityModelType>;
    };
}

const Model: ActivityModelType = {
    namespace: 'activity',
    state: {},
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
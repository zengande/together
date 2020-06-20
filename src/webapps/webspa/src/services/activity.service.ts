import config from '../../config';
import { request } from 'umi';
import Activity from '@/@types/activity/activity';

class ActivityService {
    public fetchCatalogs(): Promise<any> {
        return request(`${config.ApiBaseAddress}/catalogs`);
    }

    public getActivity(activityId: number): Promise<Activity> {

        return request(`${config.ApiBaseAddress}/activities/${activityId}`)

    }

    public getParticipants(activityId: number) { 
        return request(`${config.ApiBaseAddress}/activities/${activityId}/participants`)
    }
}

export default new ActivityService();
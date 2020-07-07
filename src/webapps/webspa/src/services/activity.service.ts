import config from '@/config';
import request from '@/utils/request';
import Activity, { ActivityInputModel } from '@/@types/activity/activity';

class ActivityService {
    public getCategories(): Promise<any> {
        return request(`${config.ApiBaseAddress}/categories`);
    }

    public getActivity(activityId: number): Promise<Activity> {

        return request(`${config.ApiBaseAddress}/activities/${activityId}`)

    }

    public getAttendees(activityId: number) {
        return request(`${config.ApiBaseAddress}/activities/${activityId}/attendees`)
    }

    public create(activity: ActivityInputModel): Promise<number | undefined> {
        return request(`${config.ApiBaseAddress}/activities`, { method: "POST", data: activity })
    }

    public join(activityId: number) {
        return request(`${config.ApiBaseAddress}/activities/${activityId}/join`, { method: "POST" })
    }

    public collect(activityId: number) {
        return request(`${config.ApiBaseAddress}/activities/${activityId}/collect`, { method: "POST" })
    }
}

export default new ActivityService();
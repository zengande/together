import request from '@/utils/request';
import config from '../../config';
import UserLocation from '@/@types/user/location';

export default {
    getUserLocation: async () => {
        const cacheKey = "together.user.location";
        try {
            var userLocation: UserLocation | null = null;
            const json = localStorage.getItem(cacheKey);
            if (json !== null && json !== '') {
                userLocation = JSON.parse(json) as UserLocation;
            }

            if (userLocation == null) {
                userLocation = await request<UserLocation>(`${config.ApiBaseAddress}/locations/user`);
                localStorage.setItem(cacheKey, JSON.stringify(userLocation))
            }

            return userLocation;
        } catch (e) {
            console.error(e);
        }
    }
}
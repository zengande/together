import request from '@/utils/request';
import config from '@/config';
import UserLocation from '@/@types/user/location';
import { getDvaApp } from 'umi';

const defaultLocation: UserLocation = {
    lat: 39.929986,
    lng: 116.395645,
    city: "北京",
    province: "北京",
    displayString: "北京",
    cityCode: 110100
}

export default {
    getUserLocation: () => {
        const cacheKey = "together.user.location";
        try {
            var userLocation: UserLocation | null = null;
            const json = localStorage.getItem(cacheKey);
            if (json !== null && json !== '') {
                userLocation = JSON.parse(json) as UserLocation;
            } else {
                if ("geolocation" in navigator) {
                    navigator.geolocation.getCurrentPosition(async position => {
                        const { coords: { latitude, longitude } } = position;
                        userLocation = await request<UserLocation>(`${config.ApiBaseAddress}/locations/reverse_geocoding?location=${latitude},${longitude}`, { getResponse: false }) || defaultLocation;
                        localStorage.setItem(cacheKey, JSON.stringify(userLocation))
                        getDvaApp()._store.dispatch({ type: 'global/updateUserLocation', payload: userLocation})
                    }, error => {
                        console.error(error)
                    })
                }
            }


            // if (userLocation == null) {
            //     userLocation = await request<UserLocation>(`${config.ApiBaseAddress}/locations/user`);
            //     localStorage.setItem(cacheKey, JSON.stringify(userLocation))
            // }

            return userLocation || defaultLocation;
        } catch (e) {
            console.error(e);
        }
    }
}
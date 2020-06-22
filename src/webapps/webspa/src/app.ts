import { RequestConfig } from 'umi';
import AuthService from '@/services/auth.service'

export const request: RequestConfig = {
    timeout: 3000,
    errorConfig: {},
    middlewares: [],
    requestInterceptors: [
        async (url, options) => {
            const accessToken = await AuthService.getAccessToken();
            const headers = {
                ...options.headers,
                "Authorization": `Bearer ${accessToken}`
            };
            console.log(headers);
            return {
                url,
                options: {
                    ...options,
                    headers
                }
            };
        }
    ],
    responseInterceptors: [
        (response, options) => {
            const codeMaps = {
                502: '网关错误。',
                503: '服务不可用，服务器暂时过载或维护。',
                504: '网关超时。',
            };
            const { status } = response;
            return response;
        }
    ],
};
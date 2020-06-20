import { RequestConfig } from 'umi';
export const request: RequestConfig = {
    timeout: 3000,
    errorConfig: {},
    middlewares: [],
    requestInterceptors: [],
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
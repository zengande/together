// import { RequestConfig, ErrorShowType } from 'umi';
// import AuthService from '@/services/auth.service';
// import { Guid } from '@/utils/guid';
// import { message } from 'antd'

// export const request: RequestConfig = {
//     timeout: 3000,
//     errorConfig: {
//         adaptor: (resData, ctx) => {
//             console.log(resData)
//             const config = {
//                 ...resData,
//                 success: resData.ok,
//                 errorMessage: resData,
//                 showType: ErrorShowType.SILENT
//             };
//             console.log(config)
//             return config;
//         },
//     },
//     middlewares: [],
//     requestInterceptors: [
//         async (url, options) => {
//             const accessToken = await AuthService.getAccessToken();
//             const headers = {
//                 ...options.headers,
//                 "x-requestid": Guid.newGuid(),
//                 "Authorization": `Bearer ${accessToken}`,
//             };
//             return {
//                 url,
//                 options: {
//                     ...options,
//                     headers
//                 }
//             };
//         }
//     ],
//     responseInterceptors: [
//         (response, options) => {
//             const codeMaps = {
//                 502: '网关错误。',
//                 503: '服务不可用，服务器暂时过载或维护。',
//                 504: '网关超时。',
//             };
//             const { status } = response;
//             if (status === 401) {
//                 message.error('请先登录')
//             }
//             return response;
//         }
//     ],
// };
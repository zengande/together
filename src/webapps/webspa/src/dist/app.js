"use strict";
exports.__esModule = true;
exports.request = void 0;
exports.request = {
    timeout: 3000,
    errorConfig: {},
    middlewares: [],
    requestInterceptors: [],
    responseInterceptors: [
        function (response, options) {
            var codeMaps = {
                502: '网关错误。',
                503: '服务不可用，服务器暂时过载或维护。',
                504: '网关超时。'
            };
            var status = response.status;
            return response;
        }
    ]
};

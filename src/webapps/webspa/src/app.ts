import { RequestConfig } from 'umi';
import config from '../config'

export const request: RequestConfig = {
    middlewares: [
        async (ctx, next) => {
            if (ctx.req.url.indexOf("[ApiBaseAddress]") === 0) {
                ctx.req.url = ctx.req.url.replace('[ApiBaseAddress]', config.ApiBaseAddress)
            }
            await next();
        }
    ]
};
import { defineConfig } from 'umi';

export default defineConfig({
    nodeModulesTransform: {
        type: 'none',
    },
    routes: [{
        path: "/",
        component: '@/layouts/index',
        routes: [
            {
                path: "/activities/create",
                wrappers: ['@/wrappers/auth'],
                title: "创建活动 - TOGETHER",
                component: 'activities/create'
            },
            {
                path: "/activities",
                title: "活动列表 - TOGETHER",
                component: 'activities/index'
            },
            {
                path: "/activities/:activityId",
                title: "活动详情 - TOGETHER",
                component: 'activities/activity/index'
            },
            {
                path: "/",
                component: 'index',
                title: "TOGETHER"
            }
        ]
    }],
    analytics: {
        baidu: 'ba26b3a231c48c6716a2447ffda9c181'
    }
});

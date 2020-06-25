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
                component: '@/layouts/AuthorizeLayout',
                routes: [{
                    path: '/activities/create',
                    title: "创建活动 - TOGETHER",
                    component: 'activities/create'
                }]
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
    }]
});

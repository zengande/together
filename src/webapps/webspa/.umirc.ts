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
          component: 'activities/create'
        }]
      },
      {
        path: "/activities",
        component: 'activities/index'
      },
      {
        path: "/activities/:activityId",

        component: 'activities/[activityId]/index'
      },
      {
        path: "/",
        component: 'index'
      }
    ]
  }]
});

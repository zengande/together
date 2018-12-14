import { Reducer } from 'redux';
import { IMenuState } from '../types/StateTypes';
import { AT } from 'src/types';

const initialState: IMenuState = {
    menus: [
        { icon: "user", text: '用户管理', link: '/management/user', children: null },
        { icon: "audit", text: '活动管理', link: '/management/activity', children: null },
        { icon: "setting", text: '系统维护', link: '/system', children: null },
        { icon: "line-chart", text: '统计信息', link: '/statistics', children: null },
        { icon: "notification", text: '消息通知', link: '/messages', children: null },
        { icon: "exception", text: '日志', link: '/logs', children: null }
    ],
    drawerVisible: false
};

export const drawerActions = {
    toggle: (isCollapsed: boolean) => ({
        type: AT.DRAWER_TOGGLE,
        isCollapsed
    })
}

export const reducer: Reducer<IMenuState> = (state = initialState, action) => {
    if (action.type === AT.DRAWER_TOGGLE) {
        return {
            ...state,
            drawerVisible: action.isCollapsed
        }
    }
    return state;
}
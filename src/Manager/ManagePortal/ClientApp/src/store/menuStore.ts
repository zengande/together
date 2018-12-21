import { Reducer } from 'redux';
import { IMenuState } from '../types/StateTypes';
import { AT } from 'src/types';

const initialState: IMenuState = {
    menus: [
        { icon: 'dashboard', text: '分析页', link: '/analysis' },
        { icon: 'schedule', text: '工作台', link: '/workplace' },
        { icon: "user", text: '用户管理', link: '/management/user' },
        { icon: "audit", text: '活动管理', link: '/management/activity' },
        { icon: "setting", text: '系统维护', link: '/system' },
        { icon: "notification", text: '消息通知', link: '/messages' },
        { icon: "exception", text: '日志', link: '/logs' },
        { icon: "message", text: 'IM', link: '/im' }
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
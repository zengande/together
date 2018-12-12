import * as React from 'react';
import { connect } from 'react-redux';
import Hero from './components/Hero';
import MainMeun from './components/MainMeun';
import Statistics from './components/Statistics';
import './Home.css';
import WorkList from './components/WorkList';
import MainLayout from '../../layouts/MainLayout';

const Home = (props: any) => {

    // const { user } = props;
    const menus = [
        { Icon: "user", Text: '用户管理', Link: '/management/user' },
        { Icon: "audit", Text: '活动管理', Link: '/management/activity' },
        { Icon: "setting", Text: '系统维护', Link: '/system' },
        { Icon: "line-chart", Text: '统计信息', Link: '/statistics' },
        { Icon: "notification", Text: '消息通知', Link: '/logs' },
        { Icon: "exception", Text: '日志', Link: '/logs' }
    ]
    return (
        <MainLayout>
            <Hero />
            <MainMeun menus={menus} />
            <Statistics />
            <WorkList />
        </MainLayout>
    );
}

export default connect(
    (state: any) => ({ user: state.user })
)(Home);

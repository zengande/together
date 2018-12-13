import * as React from 'react';
import { connect } from 'react-redux';
import Hero from './components/Hero';
import MainMeun from './components/MainMeun';
import Statistics from './components/Statistics';
import './Home.css';
import WorkList from './components/WorkList';
import MainLayout from '../../layouts/MainLayout';
import { IState } from 'src/types';

const Home = (props: any) => {

    const { menus } = props;

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
    (state: IState) => ({ menus: state.menu.menus })
)(Home);

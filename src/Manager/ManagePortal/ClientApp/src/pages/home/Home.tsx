import * as React from 'react';
import Hero from './components/Hero';
import MainMeun from './components/MainMeun';
import Statistics from './components/Statistics';
import './Home.css';
import WorkList from './components/WorkList';
import MainLayout from '../../layouts/MainLayout';

export default class Home extends React.PureComponent {
    public render() {
        return (
            <MainLayout>
                <Hero />
                <MainMeun />
                <Statistics />
                <WorkList />
            </MainLayout>
        );
    }
}
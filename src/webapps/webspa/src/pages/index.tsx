import React from 'react';
import styles from './index.less';
import ActivitiesCarousel from './components/activities-carousel';
import { connect, Loading, GlobalModelState, ConnectProps } from 'umi';
import UserLocation from '@/@types/user/location';

interface IndexPageProps extends ConnectProps {
    userLocation?: UserLocation
}

const IndexPage: React.FC<IndexPageProps> = (props: IndexPageProps) => {
    const { userLocation } =props;
    const { city } =userLocation || {};
    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <h1 className={styles.title}>与更多志趣相投的朋友一同进步</h1>
            </div>
            <div className={styles.body}>
                <div className="t-content">
                    <ActivitiesCarousel title={`${city}附近活动`} loading={true} activities={[]} />
                    <ActivitiesCarousel title="户外与冒险" loading={true} activities={[]} more="/activities" />
                    <ActivitiesCarousel title="学习" loading={true} activities={[]} />
                    <ActivitiesCarousel title="社交" loading={true} activities={[]} />
                </div>
            </div>
        </div>
    );
}

export default connect(({ loading, global }: { loading: Loading, global: GlobalModelState }) => ({
    userLocation: global.userLocation
}))(IndexPage)

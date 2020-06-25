import React from 'react';
import styles from './index.less';
import ActivitiesCarousel from './components/activities-carousel';

export default () => {
    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <h1 className={styles.title}>与你志趣相投的朋友一同进步</h1>
            </div>
            <div className={styles.body}>
                <div className="t-content">
                    <ActivitiesCarousel title="附近活动" loading={true} activities={[]}/>
                    <ActivitiesCarousel title="户外与冒险" loading={true} activities={[]}/>
                    <ActivitiesCarousel title="学习" loading={true} activities={[]}/>
                    <ActivitiesCarousel title="社交" loading={true} activities={[]}/>
                </div>
            </div>
        </div>
    );
}

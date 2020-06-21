import React from 'react';
import styles from './index.less';
import AuthService from '@/services/auth.service'
import { Link } from 'umi';

export default () => {
    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <h1 className={styles.title}>与你志趣相投的朋友一同进步</h1>
            </div>
            <div className={styles.body}>
                <div className="t-content">
                    <Link to="/activities">活动列表</Link>
                </div>
            </div>
        </div>
    );
}

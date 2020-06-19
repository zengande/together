import React from 'react';
import styles from './index.less';
import { Avatar } from 'antd'
import { Link } from 'umi';
import AuthService from '@/services/auth.service'

interface GlobalHeaderProps {
    isAuthenticated: boolean;
}

class GlobalHeader extends React.PureComponent<GlobalHeaderProps> {
    render() {
        const { isAuthenticated } = this.props;

        return (
            <div className={styles.header}>
                <div className={styles.logo}>
                    <Link to="/">
                        together
                    </Link></div>
                <div className={styles.right}>
                    <Link to="/activities/create">创建活动</Link>
                    <span className={styles.divider}></span>
                    <div className={styles.account}>
                        {
                            isAuthenticated ?
                                <Avatar /> :
                                <>
                                    <a href="javascript:void();" onClick={() => AuthService.loginRedirect()} className={styles.item}>登录</a>
                                    <Link to="" className={styles.item}>注册</Link>
                                </>
                        }
                    </div>
                </div>
            </div>
        )
    }
}

export default GlobalHeader;
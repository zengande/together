import React from 'react';
import styles from './index.less';
import AuthService from '@/services/auth.service'
import { Link } from 'umi';

export default () => {
  return (
    <div>
      <Link to="/activities">活动列表</Link>
    </div>
  );
}

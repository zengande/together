import React from 'react';
import styles from './index.less';
import AuthService from '@/services/auth.service'

export default () => {
  return (
    <div>
      <h1 className={styles.title}>Page index</h1>
      <button onClick={AuthService.loginPopup}>Login Popup</button>
      <button onClick={AuthService.getAccessToken}>Get AccessToken</button>
      <button onClick={AuthService.getAccount}>Get Account</button>
      <button onClick={AuthService.logout}>Logout</button>
    </div>
  );
}

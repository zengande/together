import React from 'react';
import AuthService from '@/services/auth.service';

export default (props: any) => {
    const isAuthenticated = AuthService.isAuthenticated();
    if (isAuthenticated) {
        return <div>{props.children}</div>;
    }
    AuthService.loginRedirect();
    return <div>正在跳转登录</div>
}
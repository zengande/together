import React from 'react';
import AuthService from '@/services/auth.service'

export default (props: any) => {
    const isSignined = AuthService.getAccount() != null;
    if (!isSignined) {
        AuthService.loginRedirect();
        return <></>;
    }
    return props.children;
}
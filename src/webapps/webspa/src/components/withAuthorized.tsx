import React from 'react';
import AuthService from '@/services/auth.service'
import { Redirect } from 'umi';

const withAuthorized = (WrappedComponent: any) => {
    return class extends React.Component {
        render() {
            const isSignined = AuthService.getAccount() != null;
            if(!isSignined){
                AuthService.loginRedirect();
                return <></>;
            }

            return  <WrappedComponent {...this.props} />
        }
    }
}

export default withAuthorized;
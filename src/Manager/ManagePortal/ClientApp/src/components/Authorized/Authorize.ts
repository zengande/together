import { connectedRouterRedirect } from 'redux-auth-wrapper/history4/redirect';
import { IState } from '../../types'


export const Authorize = connectedRouterRedirect<any, IState>({
    redirectPath: '/login',
    authenticatedSelector: state => state.identity.isAuthenticated,
    wrapperDisplayName: 'UserIsAuthenticated'
})
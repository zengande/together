import { storeService } from './storage.service';
import { IUserInfo } from 'src/types/IUserInfo';

export class SecurityService {
    public IsAuthorized = false;
    public UserData: any;

    constructor() {
        if (storeService.retrieve('IsAuthorized') !== '') {
            this.IsAuthorized = storeService.retrieve('IsAuthorized', false);
            this.UserData = storeService.retrieve('userData');
        }
    }

    public Authorize(): IUserInfo {
        this.ResetAuthorizationData();

        const userInfo: IUserInfo = {
            id: '100000',
            username: 'zengande',
            nickname: 'zeng ande'
        }

        storeService.store('IsAuthorized', true);
        storeService.store('userData', userInfo);
        return userInfo;
    }

    public GetToken(): any {
        return storeService.retrieve('authorizationData');
    }

    public ResetAuthorizationData(): void {
        storeService.store('authorizationData', '');
        storeService.store('authorizationDataIdToken', '');

        this.IsAuthorized = false;
        storeService.store('IsAuthorized', false);
    }

    public AuthorizedCallback(): void {
        console.log('call back');
    }
}

export const securityService = new SecurityService();
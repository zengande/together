import { storeService } from './storage.service';

export class SecurityService {
    public IsAuthorized = false;
    public UserData: any;

    constructor() {
        if (storeService.retrieve('IsAuthorized') !== '') {
            this.IsAuthorized = storeService.retrieve('IsAuthorized', false);
            this.UserData = storeService.retrieve('userData');
        }
    }

    public Authorize(): void {
        this.ResetAuthorizationData();

        storeService.store('IsAuthorized', true);
        storeService.store('userData', { username: 'zengande' });
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
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

    public Authorize(username: string, password: string): boolean {
        this.ResetAuthorizationData();

        // todo : send authorize request
        if (username !== 'admin' && password !== 'admin') {
            return false;
        }
        let result = true;

        storeService.store('authorizationData', "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZlNWNiNjNlZDFhMmQzNzcyZTZlNGM5MTg2M2RhYmVlIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NDQ2OTA5MzEsImV4cCI6MTU0NDY5NDUzMSwiaXNzIjoibnVsbCIsImF1ZCI6WyJudWxsL3Jlc291cmNlcyIsImFjdGl2aXRpZXMiLCJ1c2VyX2dyb3VwX2FwaSJdLCJjbGllbnRfaWQiOiJtYW5hZ2VfcG9ydGFsIiwic3ViIjoiMTNiNmRhOTMtODExMS00MTQ1LWIyOWUtOTZhMmYyN2VjMGM0IiwiYXV0aF90aW1lIjoxNTQ0NjkwOTMxLCJpZHAiOiJsb2NhbCIsInByZWZlcnJlZF91c2VybmFtZSI6InplbmdhbmRlQHFxLmNvbSIsIk5pY2tuYW1lIjoiemVuZ2FuZGUiLCJBdmF0YXIiOiJodHRwczovL3NlY3VyZS5tZWV0dXBzdGF0aWMuY29tL3Bob3Rvcy9tZW1iZXIvOS8zL2UvYy9tZW1iZXJfMjc5MDk3ODY4LmpwZWciLCJlbWFpbCI6InplbmdhbmRlQHFxLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYWN0aXZpdGllcyIsInVzZXJfZ3JvdXBfYXBpIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInB3ZCJdfQ.hT9qpTji_Fuyq8eSHrWxfz9EieLIezGtltoVxWkRGnVghdT_RDVV_-fTzuWYFMb3B1m3mSKqFBUo6ES7dgNGd4y-Gjuo_634BP6caeG6PBe8IDXJ9p5FZ5FR3n9on5aWP16yds7r4y9Jv__PC4sXpJSh3gDeKVvfZl7d5reuYzUkslkps_k7w4Gyu7UYmOj7goXgRlLQe3RigV0jR05Uh_f0hyGNYOfj47ZMGOQQpi4qu62VRpzaVjutKPhJ34LZxgXj3eInK8nKBJjIvya7b2AHDdZNSHI-mVNKC9YZVkTUkJzZ3f3QWzNJ1HTUpGhEmCTkxOXKIZa25rxXQ7jOzg");
        storeService.store('IsAuthorized', result);

        return result;
    }

    public GetUserInfo(): IUserInfo | null {
        const token = storeService.retrieve("authorizationData", '');
        if (token && token !== '') {
            const userInfo: IUserInfo = {
                id: '100000',
                username: 'zengande',
                nickname: 'zeng ande'
            }
            storeService.store('userData', userInfo);
            return userInfo;
        }
        this.ResetAuthorizationData();
        return null;
    }

    public GetToken(): any {
        return storeService.retrieve('authorizationData');
    }

    public ResetAuthorizationData(): void {
        storeService.store('authorizationData', '');
        storeService.store('authorizationDataIdToken', '');
        storeService.store('userData', '');

        this.IsAuthorized = false;
        storeService.store('IsAuthorized', false);
    }

    public AuthorizedCallback(): void {
        console.log('call back');
    }
}

export const securityService = new SecurityService();
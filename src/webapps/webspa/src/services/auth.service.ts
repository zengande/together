import * as msal from 'msal';
import AuthConfig from '@/services/auth.config';
import moment from 'moment';
import { getDvaApp } from 'umi';

const application = new msal.UserAgentApplication(AuthConfig.msalConfig);

class AuthService {

    public async loginPopup(): Promise<boolean> {
        try {
            const response = await application.loginPopup(AuthConfig.loginRequest);
            console.log("id_token acquired at: " + new Date().toString());
            console.log(response);
            service.setLoginState(true, response.expiresOn);
            return true;
        } catch (error) {
            console.error(error);
            // Error handling
            if (error.errorMessage) {
                // Check for forgot password error
                // Learn more about AAD error codes at https://docs.microsoft.com/en-us/azure/active-directory/develop/reference-aadsts-error-codes
                if (error.errorMessage.indexOf("AADB2C90118") > -1) {
                    const response = await application.loginPopup(AuthConfig.b2cPolicies.authorities.forgotPassword);
                    console.log(response);
                    window.alert("Password has been reset successfully. \nPlease sign-in with your new password.");

                }
            }
        }
        return false;
    }

    public loginRedirect() {
        application.loginRedirect(AuthConfig.loginRequest)
    }

    /**Sign-out the user */
    public logout() {
        service.setLoginState(false, new Date('1970-01-01 00:00'))
        // Removes all sessions, need to call AAD endpoint to do full logout
        application.logout();
    }

    public getAccount(): msal.Account | null {
        try {
            const account = application.getAccount();
            return account;
        } catch{
            return null;
        }
    }

    private getTokenPopup(request: msal.AuthenticationParameters) {
        return application.acquireTokenSilent(request)
            .catch(error => {
                console.error(error)
                return null;
            });
    }

    // Acquires and access token and then passes it to the API call
    public async getAccessToken(): Promise<string> {
        const tokenResponse = await service.getTokenPopup(AuthConfig.tokenRequest)
        if (tokenResponse != null) {
            const { accessToken } = tokenResponse;
            console.log("access_token acquired at: " + new Date().toString());
            console.log(accessToken);
            service.setLoginState(true, tokenResponse.expiresOn)
            return accessToken;
        }
        return '';
    }

    public isAuthenticated(): boolean {
        try {
            const result = localStorage.getItem("msal.authorize.status");
            if (result != null) {
                const { expiresOn, isAuthenticated } = <{ isAuthenticated: boolean, expiresOn: Date }>JSON.parse(result);
                const now = moment();
                return isAuthenticated &&
                    now.isBefore(expiresOn)
            }

        } catch{
        }
        return false;
    }

    public setLoginState(isAuthenticated: boolean, expiresOn: Date) {
        localStorage.setItem('msal.authorize.status', JSON.stringify({ isAuthenticated, expiresOn }));
        if (typeof (getDvaApp) === 'function') {
            getDvaApp()?._store.dispatch({ type: 'auth/save', payload: { isAuthenticated } });
        }
    }
}

const service = new AuthService();

application.handleRedirectCallback((error, response) => {
    // Error handling
    if (error) {
        console.error(error);

        // Check for forgot password error
        // Learn more about AAD error codes at https://docs.microsoft.com/en-us/azure/active-directory/develop/reference-aadsts-error-codes
        if (error.errorMessage.indexOf("AADB2C90118") > -1) {
            try {
                // Password reset policy/authority
                application.loginRedirect(AuthConfig.b2cPolicies.authorities.forgotPassword);
            } catch (err) {
                console.log(err);
            }
        }
    } else if (response != null) {
        // We need to reject id tokens that were not issued with the default sign-in policy.
        // "acr" claim in the token tells us what policy is used (NOTE: for new policies (v2.0), use "tfp" instead of "acr")
        // To learn more about b2c tokens, visit https://docs.microsoft.com/en-us/azure/active-directory-b2c/tokens-overview
        if (response.tokenType === "id_token" && response.idToken.claims['tfp'] !== AuthConfig.b2cPolicies.names.signUpSignIn) {
            application.logout();
            window.alert("Password has been reset successfully. \nPlease sign-in with your new password.");
        } else if (response.tokenType === "id_token" && response.idToken.claims['tfp'] === AuthConfig.b2cPolicies.names.signUpSignIn) {
            console.log("id_token acquired at: " + new Date().toString());
            service.setLoginState(true, response.expiresOn);
            if (application.getAccount()) {
                // todo dva
            }

        } else if (response.tokenType === "access_token") {
            console.log("access_token acquired at: " + new Date().toString());
        } else {
            console.log("Token type is: " + response.tokenType);
        }
    }
})

export default service;
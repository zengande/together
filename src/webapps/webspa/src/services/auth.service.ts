import * as msal from 'msal';
import AuthConfig from '@/services/auth.config'

const application = new msal.UserAgentApplication(AuthConfig.msalConfig);
application.handleRedirectCallback((error, response) => {
    console.log(JSON.stringify(response));
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

class AuthService {
    public async loginPopup() {
        try {
            const response = await application.loginPopup(AuthConfig.loginRequest);
            console.log("id_token acquired at: " + new Date().toString());
            console.log(response);

            const account = service.getAccount();
            console.log(account)
            if (account) {

            }
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
    }

    public loginRedirect() {
        application.loginRedirect(AuthConfig.loginRequest)
    }

    /**Sign-out the user */
    public logout() {
        // Removes all sessions, need to call AAD endpoint to do full logout
        application.logout();
    }

    public getAccount(): msal.Account {
        const account = application.getAccount();
        return account;
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
            return accessToken;
        }
        return '';
    }

    public async isAuthenticated(): Promise<boolean> {
        try {
            await application.acquireTokenSilent(AuthConfig.tokenRequest);
        } catch{
            return false;
        }
        return true;
    }
}

const service = new AuthService();

export default service;
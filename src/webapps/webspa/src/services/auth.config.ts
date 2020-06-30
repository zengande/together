import { Configuration } from 'msal';
import config from '../../config';

const baseAddress = "https://together2.b2clogin.com/together2.onmicrosoft.com";

const b2cPolicies = {
    names: {
        signUpSignIn: "B2C_1_susi",
        forgotPassword: "B2C_1_resetpwd"
    },
    authorities: {
        signUpSignIn: {
            authority: `${baseAddress}/B2C_1_susi`,
        },
        forgotPassword: {
            authority: `${baseAddress}/B2C_1_resetpwd`,
        },
    }
}

const msalConfig: Configuration = {
    auth: {
        clientId: "5f572c0d-7839-418f-a01b-71b07debf436",
        authority: b2cPolicies.authorities.signUpSignIn.authority,
        validateAuthority: false,
        redirectUri: config.AppBaseAddress,
        postLogoutRedirectUri: config.AppBaseAddress
    },
    cache: {
        cacheLocation: "localStorage", // This configures where your cache will be stored
        storeAuthStateInCookie: false // Set this to "true" to save cache in cookies to address trusted zones limitations in IE (see: https://github.com/AzureAD/microsoft-authentication-library-for-js/wiki/Known-issues-on-IE-and-Edge-Browser)
    }
};

const loginRequest = {
    scopes: ["openid", "profile"],
};
// Add here scopes for access token to be used at the API endpoints.
const tokenRequest = {
    scopes: ["https://together2.onmicrosoft.com/activityapi/user_impersonation"]
};

const AuthConfig = {
    baseAddress,
    b2cPolicies,
    msalConfig,
    loginRequest,
    tokenRequest
}

export default AuthConfig;
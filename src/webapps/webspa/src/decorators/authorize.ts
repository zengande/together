import AuthService from '@/services/auth.service';

export default function authorize(target: any, propertyKey: string, descriptor: PropertyDescriptor) {
    // keep a reference to the original function
    const originalValue = descriptor.value;

    // Replace the original function with a wrapper
    descriptor.value = async function (...args: any[]) {
        const isAuthenticated = AuthService.isAuthenticated();

        var loginResult = isAuthenticated;
        var result = null;
        if (!isAuthenticated) {
            loginResult = await AuthService.loginPopup();
        }
        if (loginResult) {
            // Call the original function
            result = originalValue.apply(this, args);
        }

        return result;
    }
}
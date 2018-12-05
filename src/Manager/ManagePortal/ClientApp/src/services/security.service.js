"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var storage_service_1 = require("./storage.service");
var SecurityService = /** @class */ (function () {
    function SecurityService() {
        this.IsAuthorized = false;
        if (storage_service_1.storeService.retrieve('IsAuthorized') !== '') {
            this.IsAuthorized = storage_service_1.storeService.retrieve('IsAuthorized', false);
            this.UserData = storage_service_1.storeService.retrieve('userData');
        }
    }
    SecurityService.prototype.Authorize = function () {
        this.ResetAuthorizationData();
        storage_service_1.storeService.store('IsAuthorized', true);
        storage_service_1.storeService.store('userData', { username: 'zengande' });
    };
    SecurityService.prototype.GetToken = function () {
        return storage_service_1.storeService.retrieve('authorizationData');
    };
    SecurityService.prototype.ResetAuthorizationData = function () {
        storage_service_1.storeService.store('authorizationData', '');
        storage_service_1.storeService.store('authorizationDataIdToken', '');
        this.IsAuthorized = false;
        storage_service_1.storeService.store('IsAuthorized', false);
    };
    return SecurityService;
}());
exports.SecurityService = SecurityService;
exports.securityService = new SecurityService();
//# sourceMappingURL=security.service.js.map
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var StorageService = /** @class */ (function () {
    function StorageService() {
        this.storage = localStorage;
    }
    StorageService.prototype.retrieve = function (key, defValue) {
        if (defValue === void 0) { defValue = null; }
        var item = this.storage.getItem(key);
        if (item && item !== 'undefined') {
            return JSON.parse(item);
        }
        return defValue;
    };
    StorageService.prototype.store = function (key, value) {
        this.storage.setItem(key, JSON.stringify(value));
    };
    return StorageService;
}());
exports.StorageService = StorageService;
exports.storeService = new StorageService();
//# sourceMappingURL=storage.service.js.map
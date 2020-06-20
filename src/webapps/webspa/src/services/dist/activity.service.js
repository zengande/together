"use strict";
exports.__esModule = true;
var config_1 = require("../../config");
var umi_1 = require("umi");
var ActivityService = /** @class */ (function () {
    function ActivityService() {
    }
    ActivityService.prototype.fetchCatalogs = function () {
        return umi_1.request(config_1["default"].ApiBaseAddress + "/catalogs");
    };
    ActivityService.prototype.getActivity = function (activityId) {
        return umi_1.request(config_1["default"].ApiBaseAddress + "/activities/" + activityId);
    };
    ActivityService.prototype.getParticipants = function (activityId) {
        return umi_1.request(config_1["default"].ApiBaseAddress + "/activities/" + activityId + "/participants");
    };
    return ActivityService;
}());
exports["default"] = new ActivityService();

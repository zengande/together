"use strict";
exports.__esModule = true;
var config_1 = require("../../config");
var request_1 = require("@/utils/request");
var ActivityService = /** @class */ (function () {
    function ActivityService() {
    }
    ActivityService.prototype.getCategories = function () {
        return request_1["default"](config_1["default"].ApiBaseAddress + "/categories");
    };
    ActivityService.prototype.getActivity = function (activityId) {
        return request_1["default"](config_1["default"].ApiBaseAddress + "/activities/" + activityId);
    };
    ActivityService.prototype.getAttendees = function (activityId) {
        return request_1["default"](config_1["default"].ApiBaseAddress + "/activities/" + activityId + "/attendees");
    };
    ActivityService.prototype.create = function (activity) {
        return request_1["default"](config_1["default"].ApiBaseAddress + "/activities", { method: "POST", data: activity });
    };
    ActivityService.prototype.join = function (activityId) {
        return request_1["default"](config_1["default"].ApiBaseAddress + "/activities/" + activityId + "/join", { method: "POST" });
    };
    ActivityService.prototype.collect = function (activityId) {
        return request_1["default"](config_1["default"].ApiBaseAddress + "/activities/" + activityId + "/collect", { method: "POST" });
    };
    return ActivityService;
}());
exports["default"] = new ActivityService();

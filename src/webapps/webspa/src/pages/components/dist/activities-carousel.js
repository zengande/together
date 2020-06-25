"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
exports.__esModule = true;
var react_1 = require("react");
var activities_carousel_less_1 = require("./activities-carousel.less");
var antd_1 = require("antd");
var classnames_1 = require("classnames");
var LoadingDiv = function (props) {
    return (react_1["default"].createElement("div", { className: activities_carousel_less_1["default"].activities }, new Array(4).fill(0).map(function (v, i) {
        return react_1["default"].createElement("div", { className: activities_carousel_less_1["default"].activity, key: i },
            react_1["default"].createElement("div", { className: classnames_1["default"](activities_carousel_less_1["default"].image, activities_carousel_less_1["default"].loading) }),
            react_1["default"].createElement("div", { className: activities_carousel_less_1["default"].info },
                react_1["default"].createElement("div", { className: classnames_1["default"](activities_carousel_less_1["default"].date, activities_carousel_less_1["default"].loading) }),
                react_1["default"].createElement("div", { className: classnames_1["default"](activities_carousel_less_1["default"].title, activities_carousel_less_1["default"].loading) }),
                react_1["default"].createElement("div", { className: classnames_1["default"](activities_carousel_less_1["default"].address, activities_carousel_less_1["default"].loading) })),
            react_1["default"].createElement("div", { className: classnames_1["default"](activities_carousel_less_1["default"].avatars, activities_carousel_less_1["default"].loading) },
                react_1["default"].createElement(antd_1.Avatar, { className: classnames_1["default"](activities_carousel_less_1["default"].avatar, activities_carousel_less_1["default"].loading), size: 30 }),
                react_1["default"].createElement(antd_1.Avatar, { className: classnames_1["default"](activities_carousel_less_1["default"].avatar, activities_carousel_less_1["default"].loading), size: 30 }),
                react_1["default"].createElement(antd_1.Avatar, { className: classnames_1["default"](activities_carousel_less_1["default"].avatar, activities_carousel_less_1["default"].loading), size: 30 })));
    })));
};
var ActivitiesCarousel = /** @class */ (function (_super) {
    __extends(ActivitiesCarousel, _super);
    function ActivitiesCarousel() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ActivitiesCarousel.prototype.render = function () {
        var _a = this.props, title = _a.title, loading = _a.loading, activities = _a.activities;
        return (react_1["default"].createElement("div", { className: activities_carousel_less_1["default"].container },
            react_1["default"].createElement("h2", { className: activities_carousel_less_1["default"].header }, title),
            react_1["default"].createElement("div", { className: activities_carousel_less_1["default"].content }, loading ? react_1["default"].createElement(LoadingDiv, null) :
                activities && activities.map(function (ativity) {
                    return react_1["default"].createElement("div", { className: activities_carousel_less_1["default"].activities });
                }))));
    };
    return ActivitiesCarousel;
}(react_1["default"].PureComponent));
exports["default"] = ActivitiesCarousel;

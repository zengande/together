"use strict";
exports.__esModule = true;
var react_1 = require("react");
var index_less_1 = require("./index.less");
var activities_carousel_1 = require("./components/activities-carousel");
exports["default"] = (function () {
    return (react_1["default"].createElement("div", { className: index_less_1["default"].container },
        react_1["default"].createElement("div", { className: index_less_1["default"].header },
            react_1["default"].createElement("h1", { className: index_less_1["default"].title }, "\u4E0E\u4F60\u5FD7\u8DA3\u76F8\u6295\u7684\u670B\u53CB\u4E00\u540C\u8FDB\u6B65")),
        react_1["default"].createElement("div", { className: index_less_1["default"].body },
            react_1["default"].createElement("div", { className: "t-content" },
                react_1["default"].createElement(activities_carousel_1["default"], { title: "\u9644\u8FD1\u6D3B\u52A8", loading: true, activities: [] }),
                react_1["default"].createElement(activities_carousel_1["default"], { title: "\u6237\u5916\u4E0E\u5192\u9669", loading: true, activities: [], more: "sd" }),
                react_1["default"].createElement(activities_carousel_1["default"], { title: "\u5B66\u4E60", loading: true, activities: [] }),
                react_1["default"].createElement(activities_carousel_1["default"], { title: "\u793E\u4EA4", loading: true, activities: [] })))));
});

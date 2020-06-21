"use strict";
exports.__esModule = true;
var react_1 = require("react");
var index_less_1 = require("./index.less");
var umi_1 = require("umi");
exports["default"] = (function () {
    return (react_1["default"].createElement("div", { className: index_less_1["default"].container },
        react_1["default"].createElement("div", { className: index_less_1["default"].header },
            react_1["default"].createElement("h1", { className: index_less_1["default"].title }, "\u4E0E\u4F60\u5FD7\u8DA3\u76F8\u6295\u7684\u670B\u53CB\u4E00\u540C\u8FDB\u6B65")),
        react_1["default"].createElement("div", { className: index_less_1["default"].body },
            react_1["default"].createElement("div", { className: "t-content" },
                react_1["default"].createElement(umi_1.Link, { to: "/activities" }, "\u6D3B\u52A8\u5217\u8868")))));
});

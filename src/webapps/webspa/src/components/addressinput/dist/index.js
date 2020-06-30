"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
exports.__esModule = true;
var react_1 = require("react");
var antd_1 = require("antd");
var react_2 = require("react");
var Cities = [{ name: '杭州', id: 1 }, { name: '上海', id: 2 }];
var Counties = [
    { "name": "临安", "id": "129379" },
    { "name": "富阳", "id": "129378" },
    { "name": "建德", "id": "129377" },
    { "name": "上城区", "id": "129367" },
    { "name": "下城区", "id": "129368" },
    { "name": "江干区", "id": "129369" },
    { "name": "拱墅区", "id": "129370" },
    { "name": "西湖区", "id": "129371" },
    { "name": "滨江区", "id": "129372" },
    { "name": "萧山区", "id": "129373" },
    { "name": "余杭区", "id": "129374" },
    { "name": "桐庐县", "id": "129375" },
    { "name": "淳安县", "id": "129376" }
];
var AddressInput = function (_a) {
    var value = _a.value, onChange = _a.onChange;
    var cities = Cities.map(function (c) { return ({ label: c.name, value: c.name }); });
    var _b = react_2.useState(Counties.map(function (c) { return ({ label: c.name, value: c.name }); })), counties = _b[0], setCounties = _b[1];
    var _city = (value || {}).city;
    if (!_city) {
        _city = cities[0].value;
    }
    var _c = react_2.useState(_city), city = _c[0], setCity = _c[1];
    var _d = react_2.useState(counties[0].value), county = _d[0], setCounty = _d[1];
    var triggerChange = function (changedValue) {
        if (onChange) {
            onChange(__assign(__assign({ city: city }, value), changedValue));
        }
    };
    return (react_1["default"].createElement("div", null,
        react_1["default"].createElement(antd_1.Row, { gutter: 12 },
            react_1["default"].createElement(antd_1.Col, { span: 8 },
                react_1["default"].createElement(antd_1.Select, { options: cities, value: city })),
            react_1["default"].createElement(antd_1.Col, { span: 8 },
                react_1["default"].createElement(antd_1.Select, { options: counties, value: county })))));
};
exports["default"] = AddressInput;

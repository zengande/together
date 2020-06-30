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
var global_header_1 = require("@/components/global-header");
var antd_1 = require("antd");
var umi_1 = require("umi");
var index_less_1 = require("./index.less");
var Content = antd_1.Layout.Content, Footer = antd_1.Layout.Footer;
var BasicLayout = /** @class */ (function (_super) {
    __extends(BasicLayout, _super);
    function BasicLayout() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    BasicLayout.prototype.componentDidMount = function () {
        this.props.dispatch({ type: 'auth/init' });
    };
    BasicLayout.prototype.render = function () {
        var children = this.props.children;
        return (react_1["default"].createElement(antd_1.Layout, { className: index_less_1["default"].layout },
            react_1["default"].createElement(global_header_1["default"], __assign({}, this.props)),
            react_1["default"].createElement(Content, { className: index_less_1["default"].content }, children),
            react_1["default"].createElement(Footer, { className: index_less_1["default"].footer }, "TOGETHER 2020")));
    };
    return BasicLayout;
}(react_1["default"].PureComponent));
exports["default"] = umi_1.connect(function (_a) {
    var loading = _a.loading, auth = _a.auth;
    return ({
        isAuthenticated: auth.isAuthenticated
    });
})(BasicLayout);

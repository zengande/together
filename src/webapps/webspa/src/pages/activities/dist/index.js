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
var umi_1 = require("umi");
var index_less_1 = require("./index.less");
var antd_1 = require("antd");
var icons_1 = require("@ant-design/icons");
var ActivitiesPage = /** @class */ (function (_super) {
    __extends(ActivitiesPage, _super);
    function ActivitiesPage() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.state = {
            query: ""
        };
        return _this;
    }
    ActivitiesPage.prototype.componentDidMount = function () {
        var q = this.props.location.query.q;
        q && this.setState({ query: q });
    };
    ActivitiesPage.prototype.render = function () {
        var query = this.state.query;
        return (react_1["default"].createElement("div", { className: index_less_1["default"].container },
            react_1["default"].createElement("div", { className: index_less_1["default"].header },
                react_1["default"].createElement("div", { className: index_less_1["default"].searchBox },
                    react_1["default"].createElement("input", { className: index_less_1["default"].input, type: "text", maxLength: 200, value: query, autoFocus: true, onChange: this.change.bind(this), onKeyDown: this.keyDown.bind(this) }),
                    react_1["default"].createElement(antd_1.Button, { className: index_less_1["default"].btn, icon: react_1["default"].createElement(icons_1.SearchOutlined, null), type: "text", onClick: this.search.bind(this) }))),
            react_1["default"].createElement("div", { className: index_less_1["default"].body },
                react_1["default"].createElement(umi_1.Link, { to: "/activities/1" }, "\u6D3B\u52A81"))));
    };
    ActivitiesPage.prototype.change = function (e) {
        var query = e.target.value;
        this.setState({ query: query });
    };
    ActivitiesPage.prototype.keyDown = function (e) {
        e.keyCode === 13 && this.search();
    };
    ActivitiesPage.prototype.search = function () {
        var query = this.state.query;
        if (query !== "") {
            alert(query);
        }
    };
    return ActivitiesPage;
}(react_1["default"].PureComponent));
exports["default"] = ActivitiesPage;

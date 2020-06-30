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
var react_syntax_highlighter_1 = require("react-syntax-highlighter");
// 设置高亮样式
var hljs_1 = require("react-syntax-highlighter/dist/esm/styles/hljs");
var CodeBlock = /** @class */ (function (_super) {
    __extends(CodeBlock, _super);
    function CodeBlock() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CodeBlock.prototype.render = function () {
        var _a = this.props, language = _a.language, value = _a.value;
        return (react_1["default"].createElement("figure", { className: "highlight" },
            react_1["default"].createElement(react_syntax_highlighter_1.PrismLight, { language: language, style: hljs_1.idea }, value)));
    };
    CodeBlock.defaultProps = {
        language: null
    };
    return CodeBlock;
}(react_1.PureComponent));
exports["default"] = CodeBlock;

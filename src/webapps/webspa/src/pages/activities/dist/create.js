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
var for_editor_1 = require("for-editor");
var antd_1 = require("antd");
var umi_1 = require("umi");
var create_less_1 = require("./create.less");
var addressinput_1 = require("@/components/addressinput");
var RangePicker = antd_1.DatePicker.RangePicker;
var CreatePage = /** @class */ (function (_super) {
    __extends(CreatePage, _super);
    function CreatePage() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.formRef = react_1["default"].createRef();
        _this.timer = null;
        return _this;
    }
    CreatePage.prototype.componentDidMount = function () {
        this.fetCatalogs();
        // this.startAutoSave();
    };
    CreatePage.prototype.componentWillUnmount = function () {
        if (this.timer !== null) {
            clearInterval(this.timer);
        }
    };
    CreatePage.prototype.fetCatalogs = function (parentId) {
        this.props.dispatch({ type: 'activitycreate/fetchCatalogs', payload: parentId });
    };
    CreatePage.prototype.startAutoSave = function () {
        var _this = this;
        this.timer = setInterval(function () {
            var _a;
            var values = (_a = _this.formRef.current) === null || _a === void 0 ? void 0 : _a.getFieldsValue();
            console.log(values);
        }, 30 * 1000);
    };
    CreatePage.prototype.render = function () {
        var formLayout = {
            labelCol: { span: 4 },
            wrapperCol: { span: 20 }
        };
        var addressVisibleRules = [{
                label: '公开',
                value: 1
            }, {
                label: "参与者可见",
                value: 2
            }];
        var toolbar = {
            h1: true,
            h2: true,
            h3: true,
            h4: true,
            img: true,
            link: true,
            code: true,
            preview: true,
            expand: true,
            /* v0.2.3 */
            subfield: true
        };
        return (react_1["default"].createElement("div", { className: create_less_1["default"].container },
            react_1["default"].createElement("div", { className: create_less_1["default"].header },
                react_1["default"].createElement("div", { className: "t-content" },
                    react_1["default"].createElement("h1", { className: create_less_1["default"].title }, "\u521B\u5EFA\u6D3B\u52A8"))),
            react_1["default"].createElement("div", { className: create_less_1["default"].body },
                react_1["default"].createElement("div", { className: "t-content" },
                    react_1["default"].createElement(antd_1.Row, { gutter: 48 },
                        react_1["default"].createElement(antd_1.Col, { span: 16 },
                            react_1["default"].createElement(antd_1.Form, __assign({}, formLayout, { ref: this.formRef, name: "control-ref", initialValues: { addressVisibleRuleId: addressVisibleRules[0].value }, onFinish: this.submit.bind(this) }),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "catalogId", label: "\u6D3B\u52A8\u7C7B\u578B", rules: [{ required: true, message: "请选择活动类型" }], wrapperCol: { span: 8 } },
                                    react_1["default"].createElement(antd_1.Select, { options: [{ label: "聚会", value: 1 }] })),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "title", label: "\u6D3B\u52A8\u6807\u9898", rules: [{ required: true, message: "请输入活动标题" }] },
                                    react_1["default"].createElement(antd_1.Input, { placeholder: "\u8BF7\u586B\u5199\u6982\u8FF0\u6D3B\u52A8\u4E3B\u9898" })),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "activityTime", label: "\u6D3B\u52A8\u65F6\u95F4", rules: [{ type: 'array', required: true, message: '请选择活动时间' }] },
                                    react_1["default"].createElement(RangePicker, { placeholder: ["开始时间", "结束时间"], showTime: true, showSecond: false, format: "YYYY-MM-DD HH:mm" })),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "endRegisterTime", label: "\u622A\u6B62\u6CE8\u518C\u65F6\u95F4", rules: [{ required: true, message: "请选择截止注册时间" }] },
                                    react_1["default"].createElement(antd_1.DatePicker, { showTime: true, showSecond: false, placeholder: "\u622A\u6B62\u65F6\u95F4", format: "YYYY-MM-DD HH:mm" })),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "limitsNum", label: "\u9650\u5236\u4EBA\u6570" },
                                    react_1["default"].createElement(antd_1.InputNumber, null)),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "address", label: "\u6D3B\u52A8\u5730\u70B9" },
                                    react_1["default"].createElement(addressinput_1["default"], null)),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "detailAddress", wrapperCol: { offset: 4 }, rules: [{ required: true, message: "请输入详细地址" }] },
                                    react_1["default"].createElement(antd_1.Input, { placeholder: "\u8BF7\u586B\u5199\u8BE6\u7EC6\u5730\u5740" })),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "addressVisibleRuleId", label: "\u5730\u5740\u53EF\u89C1\u89C4\u5219", rules: [{ required: true, message: '请选择地址可见规则' }] },
                                    react_1["default"].createElement(antd_1.Radio.Group, { options: addressVisibleRules })),
                                react_1["default"].createElement(antd_1.Form.Item, { name: "content", label: "\u6D3B\u52A8\u8BE6\u60C5", rules: [{ required: true, message: '请输入活动详情' }] },
                                    react_1["default"].createElement(for_editor_1["default"], { style: { 'boxShadow': 'none', 'borderRadius': '3px' }, lineNum: 0, toolbar: toolbar, placeholder: "\u8BF7\u586B\u5199\u672C\u6B21\u6D3B\u52A8\u5185\u5BB9...", height: "480px" })),
                                react_1["default"].createElement(antd_1.Form.Item, { wrapperCol: { offset: 4 } },
                                    react_1["default"].createElement(antd_1.Button, { type: "primary", htmlType: "submit" }, "\u521B\u5EFA\u6D3B\u52A8")))),
                        react_1["default"].createElement(antd_1.Col, { span: 8, className: create_less_1["default"].tips },
                            react_1["default"].createElement("div", { className: create_less_1["default"].rules }, "\u8BF7\u786E\u4FDD\u4F60\u7684\u6D3B\u52A8\u5408\u6CD5\uFF0C\u5426\u5219\u6D3B\u52A8\u5C06\u88AB\u4F9D\u6CD5\u5220\u9664\uFF0C\u4E14\u7531\u6B64\u9020\u6210\u7684\u540E\u679C\u987B\u7531\u6D3B\u52A8\u4E3E\u529E\u65B9\u81EA\u884C\u627F\u62C5\u3002"),
                            react_1["default"].createElement("div", { className: create_less_1["default"].info },
                                react_1["default"].createElement("h2", { className: create_less_1["default"].title }, "\u4EC0\u4E48\u662F\u5408\u9002\u7684\u540C\u57CE\u6D3B\u52A8\uFF1F"),
                                react_1["default"].createElement("ol", { className: create_less_1["default"].text },
                                    react_1["default"].createElement("li", null, "\u6709\u786E\u5B9A\u7684\u6D3B\u52A8\u8D77\u6B62\u65F6\u95F4"),
                                    react_1["default"].createElement("li", null, "\u6709\u80FD\u96C6\u4F53\u53C2\u4E0E\u7684\u6D3B\u52A8\u5730\u70B9"),
                                    react_1["default"].createElement("li", null, "\u80FD\u5728\u73B0\u5B9E\u4E2D\u78B0\u9762\u7684\u6D3B\u52A8"))),
                            react_1["default"].createElement("div", { className: create_less_1["default"].info },
                                react_1["default"].createElement("h2", { className: create_less_1["default"].title }, "\u5982\u4F55\u624D\u80FD\u8BA9\u4F60\u7684\u6D3B\u52A8\u66F4\u5438\u5F15\u4EBA\uFF1F"),
                                react_1["default"].createElement("ol", { className: create_less_1["default"].text },
                                    react_1["default"].createElement("li", null, "\u6807\u9898\u7B80\u5355\u660E\u4E86"),
                                    react_1["default"].createElement("li", null, "\u6D3B\u52A8\u5185\u5BB9\u548C\u7279\u70B9\u4ECB\u7ECD\u8BE6\u7EC6"))),
                            react_1["default"].createElement("div", { className: create_less_1["default"].info },
                                react_1["default"].createElement("h2", { className: create_less_1["default"].title }, "\u63D0\u9192"),
                                react_1["default"].createElement("ol", { className: create_less_1["default"].text },
                                    react_1["default"].createElement("li", null, "\u4E0D\u586B\u5199\u201D\u9650\u5236\u4EBA\u6570\u201C\u6216\u8005\u503C\u5C0F\u4E8E\u7B49\u4E8E0\uFF0C\u5219\u4E3A\u4E0D\u9650\u5236\u4EBA\u6570"),
                                    react_1["default"].createElement("li", null, "\u6D3B\u52A8\u8BE6\u60C5\u53EF\u4EE5\u4F7F\u7528 Markdown \u683C\u5F0F\u7F16\u8F91")))))))));
    };
    CreatePage.prototype.submit = function (values) {
        console.log(values);
    };
    return CreatePage;
}(react_1["default"].PureComponent));
exports["default"] = umi_1.connect(function (_a) {
    var loading = _a.loading, activitycreate = _a.activitycreate;
    return ({
        catalogs: activitycreate.catalogs
    });
})(CreatePage);

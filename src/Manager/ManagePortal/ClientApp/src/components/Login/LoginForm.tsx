import * as React from 'react';
import {
    Form, Icon, Input, Button, Checkbox,
} from 'antd';
import './LoginForm.css';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from 'src/store/userStore';
import { securityService } from 'src/services/security.service';

const FormItem = Form.Item;

class LoginForm extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            inProcessing: false
        }
    }

    public componentWillMount() {
        const { logout } = this.props;
        logout();
        securityService.ResetAuthorizationData();
    }

    public render() {
        const { getFieldDecorator } = this.props.form;
        return (
            <Form onSubmit={this.handleSubmit} className="login-form">
                <FormItem>
                    {getFieldDecorator('userName', {
                        rules: [{ required: true, message: 'Please input your username!' }],
                    })(
                        <Input prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />} name="username" placeholder="Username" />
                    )}
                </FormItem>
                <FormItem>
                    {getFieldDecorator('password', {
                        rules: [{ required: true, message: 'Please input your Password!' }],
                    })(
                        <Input prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />} name="password" type="password" placeholder="Password" />
                    )}
                </FormItem>
                <FormItem>
                    {getFieldDecorator('remember', {
                        valuePropName: 'checked',
                        initialValue: true,
                    })(
                        <Checkbox>Remember me</Checkbox>
                    )}
                    <a className="login-form-forgot" href="">Forgot password</a>
                    <Button disabled={this.state.inProcessing} type="primary" htmlType="submit" className="login-form-button">
                        {this.state.inProcessing ? '登录中' : '登录'}
                    </Button>
                </FormItem>
            </Form>
        )
    }

    private handleSubmit(e: any) {
        e.preventDefault();
        this.setState({ inProcessing: true });
        this.props.form.validateFields((err: any, values: any) => {
            if (!err) {
                this.login(values);
            } else {
                this.setState({ inProcessing: false });
            }
        });
    }

    private login(values: any) {

        const { login } = this.props;
        setTimeout(() => {
            let result = securityService.Authorize(values.username, values.password);
            this.setState({ inProcessing: false });
            if (result) {
                const userInfo = securityService.GetUserInfo();
                login(userInfo);
                const redirect = this.getRedirect();
                this.props.history.push(redirect)
            }
        }, 3000)

        // todo : login failed!
    }

    private getRedirect(): string {
        const returnUrl = this.getQueryString('redirect');
        if (returnUrl && returnUrl !== '') {
            return returnUrl;
        }
        return '/'
    }


    private getQueryString(name: string): string | undefined {
        const reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        let r = this.props.location.search.substr(1).match(reg);
        if (r != null) {
            return unescape(r[2]);
        }
        return undefined;
    }
}

export default connect(
    null,
    (dispatch: any) => bindActionCreators(actionCreators, dispatch)
)(Form.create()(LoginForm))
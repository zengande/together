import * as React from 'react';

export default class LoginForm extends React.Component {
    constructor(props:any){
        super(props);
    }

    public render() {
        const { } = this.props;
        return (
            <div className="login-form">
                <form>
                    <input placeholder="用户名" value={this}/>
                </form>
            </div>
        )
    }
}
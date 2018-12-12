import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from 'src/types';
class Hero extends React.Component<any> {

    public render() {

        const { userInfo } = this.props;
        const welcomeSpeech = this.getWelcomeSpeech();

        return (
            <div style={{ paddingTop: "30px" }}>
                <h1>{welcomeSpeech}，{userInfo.nickname}</h1>
            </div>
        )
    }

    private getWelcomeSpeech(): string {
        let now = new Date();
        let hour = now.getHours()
        if (hour < 6) { return "凌晨好" }
        else if (hour < 9) { return "早上好" }
        else if (hour < 12) { return "上午好" }
        else if (hour < 14) { return "中午好" }
        else if (hour < 17) { return "下午好" }
        else if (hour < 19) { return "傍晚好" }
        else { return "晚上好" }
    }

}

export default connect(
    (state: IState) => ({
        userInfo: state.identity.userInfo
    })
)(Hero)
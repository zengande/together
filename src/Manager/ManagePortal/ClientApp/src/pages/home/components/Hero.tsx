import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from 'src/types';
import { CustomIcon } from 'src/components/Icons';
class Hero extends React.PureComponent<any> {

    public render() {

        const { userInfo } = this.props;
        const welcome = this.getWelcomeSpeech();

        return (
            <div style={{ paddingTop: "30px" }}>
                <h1 className="hero-heading">{welcome.emotion && <CustomIcon className="emotion-icon" type={welcome.emotion} />}{welcome.message}，{userInfo.nickname}</h1>
            </div>
        )
    }

    private getWelcomeSpeech(): any {
        let message = '';
        let emotion = null;
        let now = new Date();
        let hour = now.getHours()
        if (hour < 6) { message = "凌晨好" }
        else if (hour < 9) {
            message = "早上好";
            emotion = 'icon-shuixing'
        }
        else if (hour < 12) {
            message = "上午好"
        }
        else if (hour < 14) {
            message = "中午好"
        }
        else if (hour < 17) {
            message = "下午好"
        }
        else if (hour < 19) {
            message = "傍晚好"
        }
        else {
            message = "晚上好"
        }
        return { message, emotion }
    }

}

export default connect(
    (state: IState) => ({
        userInfo: state.identity.userInfo
    })
)(Hero)
import * as React from 'react';
import { connect } from 'react-redux';
import { IState } from 'src/types';
import { CustomIcon } from 'src/components/Icons';
import { getWelcomeSpeech } from 'src/utils/common';
class Hero extends React.PureComponent<any> {

    public render() {

        const { userInfo } = this.props;
        const welcome = getWelcomeSpeech();

        return (
            <div style={{ paddingTop: "30px" }}>
                <h1 className="hero-heading">{welcome.emotion && <CustomIcon className="emotion-icon" type={welcome.emotion} />}{welcome.message}，{userInfo.nickname}</h1>
            </div>
        )
    }

    

}

export default connect(
    (state: IState) => ({
        userInfo: state.identity.userInfo
    })
)(Hero)
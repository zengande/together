import * as React from 'react';
import DocumentTitle from 'react-document-title';
import LoadingSpinner from '../components/LoadingSpinner/index';
import Header from '../components/Header/Header';
import LeftDrawer from 'src/components/LeftDrawer';
import { connect } from 'react-redux';
import { IState } from '../types/StateTypes';
import { bindActionCreators } from 'redux';
import { drawerActions } from 'src/store/menuStore';

class BasicLayout extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            loading: true
        }
    }

    public componentWillMount() {
        this.setState({ loading: true });
    }

    public componentDidMount() {
        setTimeout(() => {
            this.setState({ loading: false })
        }, 1000);

    }

    public render() {
        const { children, drawerVisible } = this.props;

        if (this.state.loading) {
            return (<LoadingSpinner />)
        } else {
            return (
                <DocumentTitle title={"together"}>
                    <React.Fragment>
                        <div className="main-container">
                            <Header collapsed={drawerVisible}
                                onCollapse={(isCollapsed: boolean) => this.toggle(isCollapsed)} />
                            {children}
                        </div >
                        <LeftDrawer visible={drawerVisible}
                            onCollapse={() => this.toggle(false)}/>
                    </React.Fragment>
                </DocumentTitle>
            )
        }
    }

    private toggle(visable: boolean) {
        const { toggle } = this.props;
        toggle(visable);
    }
}

export default connect(
    (state: IState) => ({ ...state.menu }),
    (dispatch: any) => bindActionCreators(drawerActions, dispatch)
)(BasicLayout);
import * as React from 'react';
import LoadingSpinner from '../components/LoadingSpinner/index';
import Header from '../components/Header/Header';
import Footer from 'src/components/Footer/Footer';
import LeftDrawer from 'src/components/LeftDrawer';
import { connect } from 'react-redux';
import { IState } from '../types/StateTypes';
import { bindActionCreators } from 'redux';
import { drawerActions } from 'src/store/menuStore';

class BasicLayout extends React.Component<any, any> {
    public static defaultProps = {
        showFooter: true
    }

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
        const { drawerVisible, children, showFooter } = this.props;

        if (this.state.loading) {
            return (<LoadingSpinner />)
        } else {
            return (
                <React.Fragment>
                    <div className="main-container">
                        <Header collapsed={drawerVisible}
                            onCollapse={(isCollapsed: boolean) => this.toggle(isCollapsed)} />

                        {children}

                        {showFooter && <Footer />}
                    </div >
                    <LeftDrawer visible={drawerVisible}
                        onCollapse={() => this.toggle(false)} />
                </React.Fragment>
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
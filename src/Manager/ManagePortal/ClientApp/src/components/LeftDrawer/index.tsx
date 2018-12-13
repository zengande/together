import * as React from 'react';
import { Drawer, Icon } from 'antd';
// import { connect } from 'react-redux';
import * as PropTypes from 'prop-types';
import DrawerMenu from './DrawerMenu';
import './LeftDrawer.css'

class LeftDrawer extends React.Component<any> {

    public static propTypes = {
        visible: PropTypes.bool,
        onCollapse: PropTypes.func
    }

    public static defaultProps = {
        visible: false
    }

    public render() {
        const { visible, onCollapse } = this.props;

        return (
            <Drawer visible={visible}
                placement="left"
                mask={true}
                width="314px"
                closable={false}
                style={{ padding: "0px",overflowX:"hidden" }}
                onClose={() => onCollapse()}>
                <div className="drawer-container">
                    <div className="header-left">
                        <Icon type={visible ? 'menu-fold' : 'menu-unfold'} onClick={() => onCollapse()} />
                    </div>
                    <div className="drawer-mainview">
                        <DrawerMenu />
                    </div>
                </div>
            </Drawer>
        )
    }
}

export default LeftDrawer;
// export default connect()(LeftDrawer);
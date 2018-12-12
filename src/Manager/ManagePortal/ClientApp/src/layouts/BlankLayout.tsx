import * as React from 'react';
import * as PropTypes from 'prop-types';

export default class BlankLayout extends React.PureComponent {
    public static propTypes = {
        children: PropTypes.node
    }

    public render() {
        const { children } = this.props;
        return (
            <div>
                {children}
            </div>
        )
    }
}
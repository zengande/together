import * as React from 'react';
import * as PropTypes from 'prop-types';
import Header from './Header';

export class Layout extends React.PureComponent {

    public static propTypes = {
        children: PropTypes.node
    }

    public render() {
        const { children } = this.props;
        return (
            <div>
                <Header />
                {children}
            </div>
        )
    }
}
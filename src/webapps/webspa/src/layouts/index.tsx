import React from 'react';
import GlobalHeader from '@/components/global-header';

class BasicLayout extends React.PureComponent {
    render() {
        const { children } = this.props;
        return (
            <>
                <GlobalHeader />
                {children}
            </>
        )
    }
}

export default BasicLayout;
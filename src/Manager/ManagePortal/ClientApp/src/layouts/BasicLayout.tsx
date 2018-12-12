import * as React from 'react';
import DocumentTitle from 'react-document-title';
import LoadingSpinner from '../components/LoadingSpinner/index';

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
        const { children } = this.props;

        if (this.state.loading) {
            return (<LoadingSpinner />)
        } else {
            return (
                <DocumentTitle title={"together"}>
                    {children}
                </DocumentTitle>
            )
        }
    }
}

export default BasicLayout;
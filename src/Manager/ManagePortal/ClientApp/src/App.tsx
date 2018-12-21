import * as React from 'react';
import './App.css';
import RouterConfig from './router';
import DocumentTitle from 'react-document-title';

class App extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            title: 'together'
        }
    }

    public render() {

        return (
            <DocumentTitle title={this.getTitle()}>
                <RouterConfig/>
            </DocumentTitle >
        );
    }

    private getTitle(): string {
        return this.state.title;
    }
}

export default App;
import * as React from 'react';

class Exception extends React.Component<any> {
    constructor(props: any) {
        super(props);

    }

    public render() {
        const { type } = this.props;
        return (<h1>{type}</h1>)
    }
}

export default Exception;
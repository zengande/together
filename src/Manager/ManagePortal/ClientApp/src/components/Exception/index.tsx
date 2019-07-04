import * as React from 'react';
import { ExceptionTypes } from 'src/types/ExceptionTypes';

class Exception extends React.Component<{ type: ExceptionTypes }> {

    public render() {
        const { type } = this.props;
        return (<h1>{type}</h1>)
    }
}

export default Exception;
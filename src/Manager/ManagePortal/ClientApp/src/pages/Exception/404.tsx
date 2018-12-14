import * as React from 'react';
import Exception from 'src/components/Exception';
import { ExceptionTypes } from 'src/types/ExceptionTypes';

const Exception404 = () => (
    <Exception
        type={ExceptionTypes.NotFound}
    />
);

export default Exception404;
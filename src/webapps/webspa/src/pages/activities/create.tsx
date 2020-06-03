import React from 'react';
import { Button } from 'antd';
import withAuthorized from '@/components/withAuthorized';

@withAuthorized
class CreatePage extends React.PureComponent {
    render() {
        return (
            <Button>Create</Button>
        )
    }
}

export default CreatePage;
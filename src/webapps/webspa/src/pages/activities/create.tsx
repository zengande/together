import React from 'react';
import { Select } from 'antd';
import { connect, Loading, CreateModelState, ConnectProps } from 'umi';
import ActivityCatalog from '@/@types/activity/catalog';

interface CreatePageProps extends ConnectProps {
    catalogs?: ActivityCatalog[]
}

class CreatePage extends React.PureComponent<CreatePageProps> {
    componentDidMount() {
        this.fetCatalogs();
    }

    private fetCatalogs(parentId?: string | number) {
        this.props.dispatch!({ type: 'activitycreate/fetchCatalogs', payload: parentId });
    }

    render() {
        console.log(this.props.catalogs);
        return (
            <div>
                <Select>
                    {this.props.catalogs && this.props.catalogs.map(c => <Select.Option value={c.id}>{c.name}</Select.Option>)}
                </Select>
            </div>
        )
    }
}

export default connect(({ loading, activitycreate }: { loading: Loading, activitycreate: CreateModelState }) => ({
    catalogs: activitycreate.catalogs
}))(CreatePage);
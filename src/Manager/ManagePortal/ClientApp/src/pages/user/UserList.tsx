import * as React from 'react'
import DataTable from 'src/components/DataTable/DataTable';

export default class UserList extends React.Component<any, any> {
    constructor(prop: any) {
        super(prop);
        this.state = {
            data: [],
            pagination: {
                total: 0,
                current: 1
            },
            loading: false,
        };
    }

    public componentDidMount() {
        this.fetch();
    }


    public render() {
        const columns = [{
            title: 'Name',
            dataIndex: 'name',
            sorter: true,
            width: '20%',
        }, {
            title: 'Age',
            dataIndex: 'age',
            // filters: [
            //     { text: 'Male', value: 'male' },
            //     { text: 'Female', value: 'female' },
            // ],
            width: '20%',
        }, {
            title: 'address',
            dataIndex: 'address',
        }];


        return (
            <div>
                <DataTable columns={columns}
                    rowKey="key"
                    dataSource={this.state.data}
                    pagination={this.state.pagination}
                    loading={this.state.loading}
                    onChange={this.handleTableChange} />

            </div>
        )
    }

    private fetch = (params = {}) => {
        this.setState({ loading: true });

        const pagination = { ...this.state.pagination };
        pagination.total = 4;
        setTimeout(() => {
            this.setState({
                loading: false,
                data: [{
                    key: '1',
                    name: 'John Brown',
                    age: 32,
                    address: 'New York No. 1 Lake Park',
                }, {
                    key: '2',
                    name: 'Joe Black',
                    age: 42,
                    address: 'London No. 1 Lake Park',
                }, {
                    key: '3',
                    name: 'Jim Green',
                    age: 32,
                    address: 'Sidney No. 1 Lake Park',
                }, {
                    key: '4',
                    name: 'Jim Red',
                    age: 32,
                    address: 'London No. 2 Lake Park',
                }],
                pagination,
            });
        }, 3000);
    }

    private handleTableChange = (pagination: any, filters: any, sorter: any) => {
        const pager = { ...this.state.pagination };
        pager.current = pagination.current;
        this.setState({
            pagination: pager,
        });
        this.fetch({
            results: pagination.pageSize,
            page: pagination.current,
            sortField: sorter.field,
            sortOrder: sorter.order,
            ...filters,
        });
    }
}
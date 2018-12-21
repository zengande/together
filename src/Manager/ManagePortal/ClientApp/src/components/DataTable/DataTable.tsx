/* tslint:disable */

import * as React from 'react';
import { Table } from 'antd';
import { ColumnProps, PaginationConfig, SorterResult } from 'antd/lib/table';
import { SpinProps } from 'antd/lib/spin';

export default class DataTable<T> extends React.Component<IDataTableProps<T>> {

    public render() {

        return (
            <Table {...this.props} size="middle" />
        )
    }
}

interface IDataTableProps<T> {
    columns: ColumnProps<{}>[] | undefined,
    rowKey?: string | ((record: T, index: number) => string);
    dataSource?: T[];
    loading?: boolean | SpinProps;
    pagination?: PaginationConfig | false;
    onChange?: (pagination: PaginationConfig, filters: Record<keyof T, string[]>, sorter: SorterResult<T>) => void;
}
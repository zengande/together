import * as React from 'react';
import { Tabs, List } from 'antd';
import { IWork } from '../types/IWork';

const TabPane = Tabs.TabPane;

export default class WorkList extends React.PureComponent<any, any> {
    constructor(props: any) {
        super(props);
        this.TabChanged = this.TabChanged.bind(this);
        this.state = {
            data: [
                { id: '1', content: 'asdf adfa sdadgsg asd!', status: 1 },
                { id: '24342', content: 'asd gfdg dgwer qwreewr!', status: 2 },
                { id: '123', content: 'xcv wer sfsage werwrer!', status: 1 },
                { id: '13435', content: 'jgh ttyu reryry!', status: 3 },
                { id: '123423', content: 'gmgfh yti were eer rr!', status: 1 },
                { id: '1', content: 'lsdfg ccf rrtre wertr rytyt wer !', status: 1 },
                { id: '2', content: 'wrwe dgfgdfw werwt dg !', status: 2 },
                { id: '3', content: 'wer wer dgdg rg dfgdg!', status: 3 }
            ],
            loading: false,
            hasMore: true,
        }
    }

    public render() {
        return (
            <div className="block-item">
                <h2 className="block-heading">工作项</h2>
                <Tabs animated={false} defaultActiveKey="1" tabBarGutter={0} onChange={this.TabChanged}>
                    <TabPane tab="待办" key="1">
                        {this.renderWorkList(1)}
                    </TabPane>
                    <TabPane tab="已完成" key="2">
                        {this.renderWorkList(2)}
                    </TabPane>
                    <TabPane tab="已关闭" key="3">
                        {this.renderWorkList(3)}
                    </TabPane>
                </Tabs>
            </div>
        );
    }

    public componentDidMount() {
        setTimeout(() => {
            // this.setState({ loading: false })
        }, 2000);
    }

    private renderWorkList(status: number) {
        return (
            <List dataSource={this.getList(status)} renderItem={(item: IWork) => (
                <List.Item key={item.id}>
                    <List.Item.Meta title={item.content} />
                </List.Item>
            )} />
        )
    }

    private getList(status: number) {
        const { data } = this.state;
        return data.filter((item: IWork) => {
            return item.status === status;
        })
    }

    private TabChanged() {
        // todo
    }
}
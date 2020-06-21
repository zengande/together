import React from 'react';
import Editor from 'for-editor'
import { Select, Row, Col, Form, Input, Button, DatePicker, InputNumber, Radio } from 'antd';
import { FormInstance } from 'antd/lib/form';
import { connect, Loading, CreateModelState, ConnectProps } from 'umi';
import ActivityCatalog from '@/@types/activity/catalog';
import styles from './create.less'
import AddressInput from '@/components/addressinput';

const { RangePicker } = DatePicker;

interface CreatePageProps extends ConnectProps {
    catalogs?: ActivityCatalog[]
}

class CreatePage extends React.PureComponent<CreatePageProps> {
    private formRef = React.createRef<FormInstance>();
    private timer: NodeJS.Timeout | null = null;

    componentDidMount() {
        this.fetCatalogs();
        // this.startAutoSave();
    }

    componentWillUnmount() {
        if (this.timer !== null) {
            clearInterval(this.timer)
        }
    }

    private fetCatalogs(parentId?: string | number) {
        this.props.dispatch!({ type: 'activitycreate/fetchCatalogs', payload: parentId });
    }

    private startAutoSave() {
        this.timer = setInterval(() => {
            const values = this.formRef.current?.getFieldsValue();
            console.log(values);
        }, 30 * 1000)
    }

    render() {
        const formLayout = {
            labelCol: { span: 4 },
            wrapperCol: { span: 20 },
        };

        const addressVisibleRules = [{
            label: '公开',
            value: 1
        }, {
            label: "参与者可见",
            value: 2
        }]

        const toolbar = {
            h1: true, // h1
            h2: true, // h2
            h3: true, // h3
            h4: true, // h4
            img: true, // 图片
            link: true, // 链接
            code: true, // 代码块
            preview: true, // 预览
            expand: true, // 全屏
            /* v0.2.3 */
            subfield: true, // 单双栏模式
        };

        return (
            <div className={styles.container}>
                <div className={styles.header}>
                    <div className="t-content">
                        <h1 className={styles.title}>创建活动</h1>
                    </div>
                </div>
                <div className={styles.body}>
                    <div className="t-content">
                        <Row gutter={48}>
                            <Col span={16}>
                                <Form {...formLayout}
                                    ref={this.formRef}
                                    name="control-ref"
                                    initialValues={{ addressVisibleRuleId: addressVisibleRules[0].value }}
                                    onFinish={this.submit.bind(this)}>
                                    <Form.Item name="catalogId" label="活动类型" rules={[{ required: true, message: "请选择活动类型" }]} wrapperCol={{ span: 8 }}>
                                        <Select options={[{ label: "聚会", value: 1 }]} />
                                    </Form.Item>
                                    <Form.Item name="title" label="活动标题" rules={[{ required: true, message: "请输入活动标题" }]}>
                                        <Input placeholder="请填写概述活动主题" />
                                    </Form.Item>
                                    <Form.Item name="activityTime" label="活动时间" rules={[{ type: 'array', required: true, message: '请选择活动时间' }]}>
                                        <RangePicker placeholder={["开始时间", "结束时间"]} showTime showSecond={false} format="YYYY-MM-DD HH:mm" />
                                    </Form.Item>
                                    <Form.Item name="endRegisterTime" label="截止注册时间" rules={[{ required: true, message: "请选择截止注册时间" }]}>
                                        <DatePicker showTime showSecond={false} placeholder="截止时间" format="YYYY-MM-DD HH:mm" />
                                    </Form.Item>
                                    <Form.Item name="limitsNum" label="限制人数">
                                        <InputNumber />
                                    </Form.Item>
                                    <Form.Item name="address" label="活动地点">
                                        <AddressInput />
                                    </Form.Item>
                                    <Form.Item name="detailAddress" wrapperCol={{ offset: 4 }} rules={[{ required: true, message: "请输入详细地址" }]}>
                                        <Input placeholder="请填写详细地址" />
                                    </Form.Item>
                                    <Form.Item name="addressVisibleRuleId" label="地址可见规则" rules={[{ required: true, message: '请选择地址可见规则' }]}>
                                        <Radio.Group options={addressVisibleRules} />
                                    </Form.Item>
                                    <Form.Item name="content" label="活动详情" rules={[{ required: true, message: '请输入活动详情' }]}>
                                        <Editor style={{ 'boxShadow': 'none', 'borderRadius': '3px' }}
                                            lineNum={0}
                                            toolbar={toolbar}
                                            placeholder="请填写本次活动内容..."
                                            height="480px" />
                                    </Form.Item>
                                    <Form.Item wrapperCol={{ offset: 4 }}>
                                        <Button type="primary" htmlType="submit">
                                            创建活动
                                        </Button>
                                    </Form.Item>
                                </Form>
                            </Col>
                            <Col span={8} className={styles.tips}>
                                <div className={styles.rules}>请确保你的活动合法，否则活动将被依法删除，且由此造成的后果须由活动举办方自行承担。</div>
                                <div className={styles.info}>
                                    <h2 className={styles.title}>什么是合适的同城活动？</h2>
                                    <ol className={styles.text}>
                                        <li>有确定的活动起止时间</li>
                                        <li>有能集体参与的活动地点</li>
                                        <li>能在现实中碰面的活动</li>
                                    </ol>
                                </div>
                                <div className={styles.info}>
                                    <h2 className={styles.title}>如何才能让你的活动更吸引人？</h2>
                                    <ol className={styles.text}>
                                        <li>标题简单明了</li>
                                        <li>活动内容和特点介绍详细</li>
                                    </ol>
                                </div>
                                <div className={styles.info}>
                                    <h2 className={styles.title}>提醒</h2>
                                    <ol className={styles.text}>
                                        <li>不填写”限制人数“或者值小于等于0，则为不限制人数</li>
                                        <li>活动详情可以使用 Markdown 格式编辑</li>
                                    </ol>
                                </div>
                            </Col>
                        </Row>
                    </div>
                </div>
            </div >
        )

    }

    private submit(values: any) {
        console.log(values);
    }
}

export default connect(({ loading, activitycreate }: { loading: Loading, activitycreate: CreateModelState }) => ({
    catalogs: activitycreate.catalogs
}))(CreatePage);
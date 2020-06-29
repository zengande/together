import React from 'react'
import { IRouteComponentProps, history, Redirect, Link, connect, Loading, ConnectProps } from 'umi';
import styles from './index.less'
import { Row, Col, Avatar, Button, Modal, List } from 'antd';
import classNames from 'classnames';
import { ActivityModelState } from '../models/activity';
import MarkdownRender from '@/components/markdown'
import Activity, { Atteandee } from '../../../@types/activity/activity';
import moment from 'moment';
import {
    ClockCircleOutlined,
    EnvironmentOutlined,
    ManOutlined,
    WomanOutlined,
    LinkOutlined,
    WechatOutlined,
    WeiboOutlined,
    StarOutlined,
    StarFilled,
    ShareAltOutlined
} from '@ant-design/icons';

const TriggerHight = 180;
moment.locale('zh-cn');

interface ActivityPageProps extends ConnectProps {
    activity?: Activity;
    attendees?: Atteandee[];
    loading?: boolean;
    joining?: boolean;
    collecting?: boolean;
}

class ActivityPage extends React.PureComponent<ActivityPageProps> {
    state = {
        activityId: 0,
        visable: false,
        fixedHeaderVisable: false
    }
    componentDidMount() {
        this.fetchActivity();
        window.addEventListener('scroll', this.handleScroll.bind(this))
    }

    private fetchActivity() {
        const { match: { params }, dispatch } = this.props;
        const activityId = Number.parseInt(params.activityId);
        if (!isNaN(activityId)) {
            this.setState({ activityId })
            dispatch!({ type: 'activity/fetch', payload: activityId });
            dispatch!({ type: 'activity/fetchAttendees', payload: activityId });
        } else {
            history.push("/activities")
        }
    }
    private handleScroll(event: Event) {
        const scrollTop = document.documentElement.scrollTop; //滚动内容高度
        const fixedHeaderVisable = scrollTop >= TriggerHight;
        this.setState((preState: any) => {
            if (preState.fixedHeaderVisable !== fixedHeaderVisable)
                return ({ fixedHeaderVisable })
        })
    }

    private collect() {
        const { dispatch } = this.props;
        const { activityId } = this.state;
        dispatch && dispatch({ type: 'activity/collect', payload: activityId })
    }

    private join() {
        const { dispatch } = this.props;
        const { activityId } = this.state;
        dispatch && dispatch({ type: 'activity/join', payload: activityId })
    }

    render() {
        const { visable, fixedHeaderVisable } = this.state;
        const {
            activity,
            loading = true,
            attendees,
            joining,
            collecting
        } = this.props;

        const {
            title,
            content,
            showAddress,
            detailAddress,
            city,
            county,
            isCollected,
            isJoined,
            numOfP = 0,
            limitsNum = 0,
            activityStartTime,
            activityEndTime,
            isCreator
        } = activity || {}

        return loading ? <div>加载中...</div> :
            activity ? (
                <div className={styles.container}>
                    <div className={styles.header}>
                        <div className={styles.content}>
                            <p className={styles.date}>{moment(activityStartTime).zone("-08:00").format("YYYY年MM月DD日, ddd, hh:mm a")}</p>
                            <h1 className={styles.title}>{title}</h1>
                            <Row>
                                <Col flex="auto" className={styles.creator}>
                                    <Avatar className={styles.avatar} size={32} />
                                    <Link className={styles.name} to="/"><b>创建者</b></Link>
                                </Col>
                                <Col flex="100px">
                                    <Button className={styles.share} type="ghost" icon={<ShareAltOutlined />} onClick={() => this.setState({ visable: true })}>分享</Button>
                                </Col>
                            </Row>
                        </div>
                    </div>
                    <div style={{ display: fixedHeaderVisable ? "block" : "none" }} className={styles.fixedHeader}>
                        <div className={styles.content}>
                            <p className={styles.date}>2020年6月25日，星期四</p>
                            <h1 className={styles.title}>{title}</h1>
                        </div>
                    </div>
                    <div className={styles.main}>
                        <Row className={styles.content}>
                            <Col span={16} className={styles.body}>
                                <h3 className={styles.sec_title}>活动详情</h3>
                                <MarkdownRender content={content} />
                            </Col>
                            <Col span={8} className={styles.float}>
                                <div className={styles.time}>
                                    <ClockCircleOutlined className={styles.icon} />
                                    <p>{moment(activityStartTime).zone("-08:00").format("YYYY年MM月DD日, ddd")}</p>
                                    <p>{moment(activityStartTime).zone("-08:00").format("HH:mm")} · {moment(activityEndTime).zone("-08:00").format("HH:mm")}</p>
                                    <a>添加到我的日历</a>
                                </div>
                                <div className={styles.address}>
                                    <EnvironmentOutlined className={styles.icon} />
                                    {!showAddress ?
                                        <>
                                            <p className="t-mosaic">地址请登录后查看</p>
                                            <p className="t-mosaic">详细地址请登录后查看</p>
                                        </> :
                                        <>
                                            <p>{city}，{county}</p>
                                            <p>{detailAddress}</p>
                                        </>
                                    }
                                </div>
                                <div className={styles.map}>
                                    <a>
                                        <img src="http://api.map.baidu.com/staticimage/v2?ak=dh2WfA8uPZ06DeLX8AINy0lYmdScllG6&mcode=666666&center=116.403874,39.914888&height=240&zoom=11" />
                                    </a>
                                </div>
                            </Col>
                        </Row>
                    </div>
                    <div className={styles.footer}>
                        <Row className={styles.content}>
                            <Col span={16}>
                                <h3 className={styles.sec_title}>参与者({numOfP})</h3>
                                <div className={styles.attendees}>
                                    {attendees && attendees.map(attendee => (
                                        <Link to={`/users/${attendee.userId}`} key={attendee.userId}>
                                            <div className={styles.attendee}>
                                                {attendee.gender === 1 ?
                                                    <WomanOutlined className={classNames(styles.gender, styles.woman)} /> :
                                                    <ManOutlined className={classNames(styles.gender, styles.man)} />
                                                }
                                                <Avatar size={64} className={styles.avatar} />
                                                <p className={styles.name}>{attendee.nickname}</p>
                                                <p className={styles.role}>{attendee.isOwner ? "创建者" : "成员"}</p>
                                            </div>
                                        </Link>
                                    ))}
                                </div>
                            </Col>
                        </Row>
                    </div>
                    <div className={styles.stickyFooter}>
                        <Row className={styles.content}>
                            <Col span={16}>
                                <p className={styles.date}>{moment(activityStartTime).zone("-08:00").format("YYYY年MM月DD日, ddd, hh:mm a")}</p>
                                <p className={styles.title}>{title}</p>
                            </Col>
                            <Col span={8} className={styles.right}>
                                <div className={styles.info}>
                                    <b style={{ lineHeight: limitsNum > 0 ? 'unset' : '50px' }}>免费</b>
                                    {limitsNum > 0 && <span>剩余 {limitsNum - numOfP} 个席位</span>}
                                </div>
                                {isCreator ?
                                    <Button className={styles.cancelBtn} danger type="primary">取消活动</Button> :
                                    <>
                                        <Button className={styles.starBtn}
                                            icon={isCollected ? <StarFilled style={{ color: '#1890ff' }} /> : <StarOutlined />}
                                            type="ghost"
                                            loading={collecting}
                                            onClick={this.collect.bind(this)} />
                                        <Button className={styles.joinBtn} type="primary" loading={joining} onClick={this.join.bind(this)}>{isJoined ? "已加入" : "加入活动"}</Button>
                                    </>
                                }
                            </Col>
                        </Row>
                    </div>

                    <Modal title="分享此活动"
                        footer={null}
                        visible={visable}
                        onCancel={() => this.setState({ visable: false })}>
                        <List>
                            <List.Item><WechatOutlined /> 分享到朋友圈</List.Item>
                            <List.Item><WeiboOutlined /> 分享到微博</List.Item>
                            <List.Item><LinkOutlined /> 复制连接</List.Item>
                        </List>
                    </Modal>
                </div >
            ) : <Redirect to="/activities" />
    }
}

export default connect(({ loading, activity }: { loading: Loading, activity: ActivityModelState }) => ({
    ...activity,
    loading: loading.effects['activity/fetch'],
    joining: loading.effects['activity/join'],
    collecting: loading.effects['activity/collect'],
}))(ActivityPage);
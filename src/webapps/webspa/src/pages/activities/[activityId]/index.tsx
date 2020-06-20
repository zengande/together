import React from 'react'
import { IRouteComponentProps, history, Redirect, Link, connect, Loading, ConnectProps } from 'umi';
import styles from './index.less'
import { Row, Col, Avatar, Button, Modal, List } from 'antd';
import { ClockCircleOutlined, EnvironmentOutlined, ManOutlined, WomanOutlined, LinkOutlined, WechatOutlined, WeiboOutlined, StarOutlined, ShareAltOutlined } from '@ant-design/icons';
import classNames from 'classnames';
import ReactMarkdown from 'react-markdown'
import { ActivityModelState } from '../models/activity';
import Activity from '@/@types/activity/activity';
import { Participant } from '../../../@types/activity/activity';

const TriggerHight = 180;

interface ActivityPageProps extends ConnectProps {
    activity?: Activity;
    participants?: Participant[];
    loading?: boolean;
}

class ActivityPage extends React.PureComponent<ActivityPageProps> {
    state = {
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
            dispatch!({ type: 'activity/fetch', payload: activityId });
            dispatch!({ type: 'activity/fetchParticipants', payload: activityId });
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

    render() {
        const { visable, fixedHeaderVisable } = this.state;
        const { activity, loading = true, participants } = this.props;

        const {
            title,
            content
        } = activity || {}

        return loading ? <div>加载中...</div> :
            activity ? (
                <div className={styles.container}>
                    <div className={styles.header}>
                        <div className={styles.content}>
                            <p className={styles.date}>2020年6月25日，星期四</p>
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
                                <ReactMarkdown source={content} />
                            </Col>
                            <Col span={8} className={styles.float}>
                                <div className={styles.time}>
                                    <ClockCircleOutlined className={styles.icon} />
                                    <p>2020年06月22日，星期一</p>
                                    <p>08:00 · 12:00</p>
                                    <a>添加到我的日历</a>
                                </div>
                                <div className={styles.address}>
                                    <EnvironmentOutlined className={styles.icon} />
                                    <p>人民大会堂</p>
                                    <p>中国，北京，北京 左转第一个红绿灯的天桥下 o(*￣▽￣*)o</p>
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
                                <h3 className={styles.sec_title}>参与者({participants?.length})</h3>
                                <div className={styles.participants}>
                                    {participants ? participants.map(participant => (
                                        <Link to={`/users/${participant.userId}`} key={participant.userId}>
                                            <div className={styles.participant}>
                                                {participant.gender === 1 ?
                                                    <WomanOutlined className={classNames(styles.gender, styles.woman)} /> :
                                                    <ManOutlined className={classNames(styles.gender, styles.man)} />
                                                }
                                                <Avatar size={64} className={styles.avatar} />
                                                <p className={styles.name}>{participant.nickname}</p>
                                                <p className={styles.role}>{participant.isOwner ? "创建者" : "成员"}</p>
                                            </div>
                                        </Link>
                                    )) : <></>}
                                </div>
                            </Col>
                        </Row>
                    </div>
                    <div className={styles.stickyFooter}>
                        <Row className={styles.content}>
                            <Col span={16}>
                                <p className={styles.date}>2020年6月25日，星期四，16:00</p>
                                <p className={styles.title}>{title}</p>
                            </Col>
                            <Col span={8} className={styles.right}>
                                <div className={styles.info}>
                                    <b style={{ lineHeight: '50px' }}>免费</b>
                                    {/* <span>剩余 2 个席位</span> */}
                                </div>
                                <Button className={styles.starBtn} icon={<StarOutlined />} type="ghost" />
                                <Button className={styles.joinBtn} type="primary">加入活动</Button>
                                {/* <Button className={styles.cancelBtn} type="danger">退出活动</Button> */}
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
                </div>
            ) : <Redirect to="/activities" />
    }
}

export default connect(({ loading, activity }: { loading: Loading, activity: ActivityModelState }) => ({
    ...activity,
    loading: loading.effects['activity/fetch']
}))(ActivityPage);
import React from 'react'
import { IRouteComponentProps, history, Redirect } from 'umi';
import styles from './index.less'
import { Row, Col, Avatar, Button } from 'antd';
import { ClockCircleOutlined, EnvironmentOutlined, ManOutlined, WomanOutlined, CheckOutlined, CloseOutlined, ShareAltOutlined } from '@ant-design/icons';
import classNames from 'classnames';
import ReactMarkdown from 'react-markdown'

const content = `
# 使用 Jenkins 从 GitHub 部署到 AKS
通过 Jenkins 中的持续集成（CI）和持续部署（CD），拉取 GitHub 代码部署到 AKS 集群。

## 先决条件
* 安装 Docker、kubectl、helm
* 安装并配置 [azure cli](https://docs.microsoft.com/zh-cn/cli/azure/install-azure-cli)
## 在虚拟机中部署 Jenkins
### 安装OpenJDK 10 JDK
\`$ sudo apt install default-jdk\`
### 下载并安装 [Jenkins](https://www.jenkins.io/)
\`\`\`
$ mkdir jenkins && cd jenkins
$ wget https://pkg.jenkins.io/debian-stable/binary/jenkins_2.222.4_all.deb
$ sudo apt-get install daemon
$ sudo dpkg -i jenkins_2.222.4_all.deb
\`\`\`
## 在 Jenkins 中创建 docker hub 凭据
依次点击 Jenkins \> 凭据 \> 系统 \> 全局凭据(unrestricted) > 添加凭据（地址\`http://<JENKINS-IP>:8080/credentials/store/system/domain/_/newCredentials\`），凭据类型为 \`Username with password\`，并依次填入以下信息：
* 用户名：登录 docker hub 的用户名
* 密码
* ID：凭据标识
`;


interface ActivityPageProps extends IRouteComponentProps<{ activityId: string }> {

}

class ActivityPage extends React.PureComponent<ActivityPageProps> {
    state = { activityId: 0 }
    componentDidMount() {
        const { match: { params } } = this.props;
        const activityId = Number.parseInt(params.activityId);
        this.setState({ activityId })
    }

    render() {
        const { activityId } = this.state;

        return isNaN(activityId) ? <Redirect to='/activities' /> : (
            <div className={styles.container}>
                <div className={styles.header}>
                    <Row className={styles.content}>
                        <Col span={16}>
                            <h1>活动大标题 ——  活动大标题活动大标题活动大标题</h1>
                        </Col>
                        <Col span={8}>
                            <div className={styles.join}>
                                <p className={styles.title}>参加此活动?</p>
                                <Row gutter={24}>
                                    <Col span={12}><Button className={styles.btn} type="primary" icon={<CheckOutlined />} /></Col>
                                    <Col span={12}><Button className={styles.btn} type="primary" icon={<CloseOutlined />} /></Col>
                                </Row>
                            </div>
                            <div className={styles.share}>
                                <Button type="text" icon={<ShareAltOutlined />}>分享</Button>
                            </div>
                        </Col>
                    </Row>
                </div>
                <div className={styles.main}>
                    <Row className={styles.content}>
                        <Col span={16} className={styles.body}>
                            <h3 className={styles.sec_title}>活动详情</h3>
                            <ReactMarkdown source={content}/>
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
                                        <img />
                                    </a>
                                </div>
                            </Col>
                    </Row>
                </div>
                    <div className={styles.footer}>
                        <Row className={styles.content}>
                            <Col span={16}>
                                <h3 className={styles.sec_title}>参与者(5)</h3>
                                <div className={styles.participants}>
                                    {[1, 2, 3, 4, 5, 6, 7, 8, 9].map(v => (
                                        <a key={v}>
                                            <div className={styles.participant}>
                                                {v % 3 === 0 ?
                                                    <WomanOutlined className={classNames(styles.gender, styles.woman)} /> :
                                                    <ManOutlined className={classNames(styles.gender, styles.man)} />
                                                }
                                                <Avatar size={64} className={styles.avatar} />
                                                <p className={styles.name}>参与者{v}</p>
                                            </div>
                                        </a>
                                    ))}
                                </div>
                            </Col>
                        </Row>
                    </div>
                </div>
        )
    }
}

export default ActivityPage;
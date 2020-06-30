import React from 'react';
import styles from './activities-carousel.less';
import { Row, Col, Avatar } from 'antd'
import classNames from 'classnames';
import { Link } from 'umi';

interface ActivitiesCarouselProps {
    title: string;
    loading?: boolean;
    activities?: any[];
    more?: string;
}

const LoadingDiv: React.FC = (props) => {
    return (
        <div className={styles.activities}>
            {new Array(4).fill(0).map((v, i) =>
                <div className={styles.activity} key={i}>
                    <div className={classNames(styles.image, styles.loading)}></div>
                    <div className={styles.info}>
                        <div className={classNames(styles.date, styles.loading)}></div>
                        <div className={classNames(styles.title, styles.loading)}></div>
                        <div className={classNames(styles.address, styles.loading)}></div>
                    </div>
                    <div className={classNames(styles.avatars, styles.loading)}>
                        <Avatar className={classNames(styles.avatar, styles.loading)} size={30} />
                        <Avatar className={classNames(styles.avatar, styles.loading)} size={30} />
                        <Avatar className={classNames(styles.avatar, styles.loading)} size={30} />
                    </div>
                </div>
            )}
        </div>
    )
}

class ActivitiesCarousel extends React.PureComponent<ActivitiesCarouselProps>{
    render() {
        const { title, loading, activities, more } = this.props;

        return (
            <div className={styles.container}>
                <h2 className={styles.header}>{title}
                    {more && <Link className={styles.more} to={more}>更多</Link>}
                </h2>
                <div className={styles.content}>
                    {loading ? <LoadingDiv /> :
                        activities && activities.map(ativity =>
                            <div className={styles.activities}>

                            </div>
                        )}
                </div>
            </div>
        )
    }
}

export default ActivitiesCarousel;
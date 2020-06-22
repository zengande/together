import React from 'react'
import { Link, IRouteComponentProps } from 'umi';
import styles from './index.less'
import { Button } from 'antd';
import { SearchOutlined } from '@ant-design/icons';

interface ActivitiesPageProps extends IRouteComponentProps<any, { q?: string }> {

}

class ActivitiesPage extends React.PureComponent<ActivitiesPageProps> {
    state = {
        query: ""
    }

    componentDidMount() {
        const { location: { query: { q } } } = this.props;
        q && this.setState({ query: q })
    }

    render() {
        const { query } = this.state;

        return (
            <div className={styles.container}>
                <div className={styles.header}>
                    <div className={styles.searchContainer}>
                        <div className={styles.searchBox}>
                            <input
                                className={styles.input}
                                type="text"
                                maxLength={200}
                                value={query}
                                autoFocus={true}
                                onChange={this.change.bind(this)}
                                onKeyDown={this.keyDown.bind(this)} />
                            <Button
                                className={styles.btn}
                                icon={<SearchOutlined />}
                                type="text"
                                onClick={this.search.bind(this)} />
                        </div>
                        <div className={styles.searchFields}>
                        </div>
                    </div>
                </div>
                <div className={styles.body}>
                    <Link to="/activities/2">活动2</Link>
                </div>
            </div>
        )
    }

    private change(e: React.ChangeEvent<HTMLInputElement>) {
        const query = e.target.value;
        this.setState({ query })
    }
    private keyDown(e: React.KeyboardEvent<HTMLInputElement>) {
        e.keyCode === 13 && this.search();
    }
    private search() {
        const { query } = this.state;
        if (query !== "") {
            alert(query)
        }
    }
}

export default ActivitiesPage;
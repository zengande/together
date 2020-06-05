import React from 'react'
import { IRouteComponentProps, history, Redirect } from 'umi'

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
            <div>{this.state.activityId}</div>
        )
    }
}

export default ActivityPage;
import * as React from 'react';
import './Apps.css';
import { Link } from 'react-router-dom';
import { IApp } from '../types/IApp';

class App extends React.PureComponent<{ app: IApp }>{
    public render() {
        const { app } = this.props;
        return (
            <Link to={app.Link} className="app-item" >
                <span>{app.Text.substr(0, 1)}</span>
                <div>
                    <div className="app-item-text">{app.Text}</div>
                </div>
            </Link>
        );
    }
}

export default class Apps extends React.Component<{ apps: IApp[] }> {
    public render() {
        const { apps } = this.props;
        return (
            <div className="apps-container">
                <h2 className="apps-heading">应用</h2>
                <div className="apps">
                    {
                        (apps && apps.length > 0) ?
                            apps.map((value: IApp, index: number) => {
                                return (<App key={index} app={value} />);
                            }) : (
                                <div className="no-result"> 没有应用 </div>
                            )
                    }
                </div>
            </div>
        )

    }
}


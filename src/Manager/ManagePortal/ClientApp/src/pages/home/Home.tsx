import * as React from 'react';
import { connect } from 'react-redux';
import Hero from './components/Hero';
import Apps from './components/Apps';

const Home = (props: any) => {

    // const { user } = props;
    const apps = [
        { Text: 'Outlook', Link: '/sadf/asdfsd' },
        { Text: 'OneDrive', Link: '/sadf/asdfsd' },
        { Text: 'Word', Link: '/sadf/asdfsd' },
        { Text: 'Excel', Link: '/sadf/asdfsd' },
        { Text: 'PowerPoint', Link: '/sadf/asdfsd' },
        { Text: 'OneNote', Link: '/sadf/asdfsd' },
        { Text: 'SharePoint', Link: '/sadf/asdfsd' },
        { Text: 'Teams', Link: '/sadf/asdfsd' },
        { Text: 'Yammer', Link: '/sadf/asdfsd' },
        { Text: 'Dynamics 365', Link: '/sadf/asdfsd' },
        { Text: 'Flow', Link: '/sadf/asdfsd' },
    ]
    return (
        <div>
            <Hero />
            <Apps apps={apps} />
        </div>
    );
}

export default connect(
    (state: any) => ({ user: state.user })
)(Home);

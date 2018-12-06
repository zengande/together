import * as React from 'react';
import * as ReactDOM from 'react-dom';
import App from './App';
import './index.css';
import registerServiceWorker from './registerServiceWorker';
import { createBrowserHistory } from 'history'
import configureStore from './store/configureStore';
import { Provider } from 'react-redux';
// import { Router } from 'react-router';
import { BrowserRouter } from 'react-router-dom'
// import { ConnectedRouter } from 'react-router-redux';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') || undefined;
const rootElement = document.getElementById('root');

const history = createBrowserHistory({ basename: baseUrl });

const store = configureStore(history);

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </Provider>,
    rootElement);
registerServiceWorker();

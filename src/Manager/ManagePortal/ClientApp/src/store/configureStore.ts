import { combineReducers, createStore, applyMiddleware } from 'redux';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as userStore from './userStore';
import * as menuStote from './menuStore';
import * as globalStore from './globalStore'
import thunk from 'redux-thunk';
import { History } from 'history';

export default function configureStore(history: History<any>) {
    const reducers = {
        identity: userStore.reducer,
        menu: menuStote.reducer,
        global: globalStore.reducer
    };

    const middleware = [
        thunk,
        routerMiddleware(history)
    ];

    const rootReducer = combineReducers({
        ...reducers,
        routing: routerReducer
    });

    return createStore(
        rootReducer,
        applyMiddleware(...middleware)
    );
}
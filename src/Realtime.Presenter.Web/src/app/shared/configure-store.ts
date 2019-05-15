import {createStore, applyMiddleware} from 'redux';
import {History} from 'history'
import {createEpicMiddleware} from "redux-observable";
import {composeWithDevTools} from 'redux-devtools-extension';
import {routerMiddleware} from "connected-react-router";

import {createRootEpic} from "./root-epic";
import {createRootReducer} from "./root-reducer";

export function configureStore(history: History) {
    const epicMiddleware = createEpicMiddleware();
    const store = createStore(
        createRootReducer(history),
        composeWithDevTools(
            applyMiddleware(
                epicMiddleware,
                routerMiddleware(history)
            )
        )
    );
    epicMiddleware.run(createRootEpic());
    return store;
}

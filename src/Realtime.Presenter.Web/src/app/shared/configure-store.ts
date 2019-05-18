import {createStore, applyMiddleware, Action} from 'redux';
import {History} from 'history'
import {createEpicMiddleware} from "redux-observable";
import {composeWithDevTools} from 'redux-devtools-extension';
import {routerMiddleware} from "connected-react-router";

import {createRootEpic} from "./root-epic";
import {createRootReducer} from "./root-reducer";
import {AppState} from "./app-state";
import {EpicDependencies} from "./epic-dependencies";
import {createSignalRMiddleware} from "./signalr/signalr-middleware";
import {createHubConnection} from "./signalr/create-hub-connection";

export function configureStore(history: History) {
    const epicMiddleware = createEpicMiddleware<Action, Action, AppState, EpicDependencies>({});
    const store = createStore(
        createRootReducer(history),
        composeWithDevTools(
            applyMiddleware(
                epicMiddleware,
                routerMiddleware(history),
                createSignalRMiddleware(createHubConnection),
            )
        )
    );
    epicMiddleware.run(createRootEpic());
    return store;
}

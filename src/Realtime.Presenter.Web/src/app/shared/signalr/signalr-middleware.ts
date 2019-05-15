import {Credentials} from "../models";
import {HubConnection} from "@aspnet/signalr";
import {AnyAction, Dispatch, Middleware, Store} from "redux";
import {AppState} from "../app-state";
import {SharedActionTypes} from "../actions";
import {PayloadAction} from "typesafe-actions";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";

export function createSignalRMiddleware(createHubConnection: (credentials: Credentials) => HubConnection): Middleware {
    let hubConnection: HubConnection = null;
    return (store: Store<AppState>) => (next: Dispatch<AnyAction>) => async (action: AnyAction) =>{
        switch (action.type) {
            case SharedActionTypes.LOAD_CREDENTIALS_SUCCESS:
                hubConnection = await createAndStartSignalRConnection(<PayloadAction<string, Credentials>>action, createHubConnection, store);
                break;
        }

        return next(action);
    };
}

async function createAndStartSignalRConnection(
    action: PayloadAction<string, Credentials>,
    createHubConnection: (credentials: Credentials) => HubConnection,
    store: Store<AppState>): Promise<HubConnection> {
    const connection = createHubConnection(action.payload);
    connection.on('nextSlide', () => store.dispatch(goToNextSlide()));
    connection.on('previousSlide', () => store.dispatch(goToPreviousSlide()));
    await connection.start();
    return connection;
}



import {AnyAction, Dispatch, Middleware, Store} from "redux";
import {PayloadAction} from "typesafe-actions";
import {HubConnection} from "@aspnet/signalr";

import {Credentials} from "../models";
import {AppState} from "../app-state";
import {SharedActionTypes} from "../actions";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";
import {SettingsActionTypes} from "../../settings/actions";

export function createSignalRMiddleware(createHubConnection: (credentials: Credentials) => HubConnection): Middleware {
    let hubConnection: HubConnection = null;
    return (store: Store<AppState>) => (next: Dispatch<AnyAction>) => async (action: AnyAction) =>{
        switch (action.type) {
            case SharedActionTypes.LOAD_CREDENTIALS_SUCCESS:
                hubConnection = await createAndStartSignalRConnection(<PayloadAction<string, Credentials>>action, createHubConnection, store);
                break;
            case SettingsActionTypes.UPDATED:
                await stopHubConnection(hubConnection);
                break;
        }

        return next(action);
    };
}

async function stopHubConnection(hubConnection: HubConnection) {
    if (hubConnection) {
        await hubConnection.stop();
    }
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



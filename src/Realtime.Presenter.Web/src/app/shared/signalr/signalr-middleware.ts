import {AnyAction, Dispatch, Middleware, Store} from "redux";
import {PayloadAction} from "typesafe-actions";
import {HubConnection} from "@aspnet/signalr";

import {AppState} from "../app-state";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";
import {SettingsActionTypes} from "../../settings/actions";
import {SettingsModel} from '../../settings/models';

export function createSignalRMiddleware(createHubConnection: (settings: SettingsModel) => HubConnection): Middleware {
    let hubConnection: HubConnection = null;
    return (store: Store<AppState>) => (next: Dispatch) => async (action: AnyAction) =>{
        if (action.type === SettingsActionTypes.UPDATED) {
            await stopHubConnection(hubConnection);
            hubConnection = await createAndStartSignalRConnection(<PayloadAction<string, SettingsModel>>action, createHubConnection, store);
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
    action: PayloadAction<string, SettingsModel>,
    createHubConnection: (credentials: SettingsModel) => HubConnection,
    store: Store<AppState>): Promise<HubConnection> {
    const connection = createHubConnection(action.payload);
    connection.on('nextSlide', () => store.dispatch(goToNextSlide()));
    connection.on('previousSlide', () => store.dispatch(goToPreviousSlide()));
    await connection.start();
    return connection;
}



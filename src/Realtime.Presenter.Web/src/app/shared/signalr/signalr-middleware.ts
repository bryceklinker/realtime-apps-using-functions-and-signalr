import {AnyAction, Dispatch, Middleware, Store} from "redux";
import {PayloadAction} from "typesafe-actions";
import {HubConnection} from "@aspnet/signalr";

import {AppState} from "../app-state";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";
import {SettingsActionTypes} from "../../settings/actions";
import {SettingsModel} from '../../settings/models';
import {SharedActionTypes} from '../actions';
import {selectSettings} from '../../settings/reducers/settings-reducer';

export function createSignalRMiddleware(createHubConnection: (settings: SettingsModel) => HubConnection): Middleware {
    let hubConnection: HubConnection = null;
    
    return (store: Store<AppState>) => (next: Dispatch) => async (action: AnyAction) => {
        switch (action.type) {
            case SharedActionTypes.INITIALIZE:
                hubConnection = await createAndStartSignalRConnection(selectSettings(store.getState()), createHubConnection, store);
                break;
            case SettingsActionTypes.UPDATED:
                await stopHubConnection(hubConnection);
                hubConnection = await createAndStartSignalRConnection(action.payload, createHubConnection, store);
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
    settings: SettingsModel,
    createHubConnection: (credentials: SettingsModel) => HubConnection,
    store: Store<AppState>): Promise<HubConnection> {
    const connection = createHubConnection(settings);
    connection.on('nextSlide', () => store.dispatch(goToNextSlide()));
    connection.on('previousSlide', () => store.dispatch(goToPreviousSlide()));
    await connection.start();
    return connection;
}



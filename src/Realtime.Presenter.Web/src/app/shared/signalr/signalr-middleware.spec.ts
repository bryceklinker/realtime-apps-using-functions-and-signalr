import {MockStore} from "redux-mock-store";

import {AppState} from "../app-state";
import {configureMockStore} from "../../../testing/configure-mock-store";
import {createSignalRMiddleware} from "./signalr-middleware";
import {FakeHubConnection} from "../../../testing/create-fake-hub-connection";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";
import {settingsUpdated} from "../../settings/actions";
import {waitForPromises} from "../../../testing/wait-for-promises";
import {SettingsModel} from '../../settings/models';
import {AccountBalanceWallet} from '@material-ui/icons';

describe('signalRMiddleware', () => {
    let settings: SettingsModel;
    let store: MockStore<AppState>;
    let hubConnection: FakeHubConnection;
    let createHubConnection: jest.Mock;

    beforeEach(() => {
        hubConnection = new FakeHubConnection();
        createHubConnection = jest.fn().mockReturnValue(hubConnection);
        settings = {
            baseUrl: 'https://signalr.com'
        };

        store = configureMockStore([createSignalRMiddleware(createHubConnection)]);
    });

    it('should create signalr hub', async () => {
        store.dispatch(settingsUpdated(settings));
        
        await waitForPromises();
        
        expect(createHubConnection).toHaveBeenCalledWith(settings);
    });

    it('should start signalr connection', async () => {
        store.dispatch(settingsUpdated(settings));

        await waitForPromises();
        
        expect(hubConnection.start).toHaveBeenCalled();
    });

    it('should stop signalr connection when settings are changed', async () => {
        store.dispatch(settingsUpdated(settings));
        await waitForPromises();

        store.dispatch(settingsUpdated({baseUrl: 'something'}));
        await waitForPromises();
        
        expect(hubConnection.stop).toHaveBeenCalled();
    });

    it('should dispatch next slide action', async () => {
        store.dispatch(settingsUpdated(settings));
        await waitForPromises();
        
        hubConnection.trigger('nextSlide');
        expect(store.getActions()).toContainEqual(goToNextSlide());
    });

    it('should dispatch previous slide action', async () => {
        store.dispatch(settingsUpdated(settings));
        await waitForPromises();
        
        hubConnection.trigger('previousSlide');
        expect(store.getActions()).toContainEqual(goToPreviousSlide());
    });
});

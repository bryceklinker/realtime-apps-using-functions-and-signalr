import {MockStore} from "redux-mock-store";

import {AppState} from "../app-state";
import {configureMockStore} from "../../../testing/configure-mock-store";
import {createSignalRMiddleware} from "./signalr-middleware";
import {FakeHubConnection} from "../../../testing/create-fake-hub-connection";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";
import {settingsUpdated} from "../../settings/actions";
import {waitForPromises} from "../../../testing/wait-for-promises";
import {SettingsModel} from '../../settings/models';
import {initialize} from '../actions';
import {selectSettings} from '../../settings/reducers/settings-reducer';

describe('signalRMiddleware', () => {
    let initialSettings: SettingsModel;
    let updatedSettings: SettingsModel;
    let store: MockStore<AppState>;
    let hubConnection: FakeHubConnection;
    let createHubConnection: jest.Mock;

    beforeEach(() => {
        hubConnection = new FakeHubConnection();
        createHubConnection = jest.fn().mockReturnValue(hubConnection);
        store = configureMockStore([createSignalRMiddleware(createHubConnection)]);

        initialSettings = selectSettings(store.getState());
        updatedSettings = {
            baseUrl: 'http://something.com'
        };
        
        store.dispatch(initialize());
    });

    it('should start hub when initialized', async () => {
        await waitForPromises();
        
        expect(createHubConnection).toHaveBeenCalledWith(initialSettings);
        expect(hubConnection.start).toHaveBeenCalled();
    });
    
    it('should restart signalr hub when settings are updated', async () => {
        store.dispatch(settingsUpdated(updatedSettings));
        
        await waitForPromises();
        
        expect(hubConnection.stop).toHaveBeenCalled();
        expect(hubConnection.start).toHaveBeenCalled();
        expect(createHubConnection).toHaveBeenCalledWith(updatedSettings);
    });

    it('should dispatch next slide action', async () => {
        hubConnection.trigger('nextSlide');
        expect(store.getActions()).toContainEqual(goToNextSlide());
    });

    it('should dispatch previous slide action', async () => {
        hubConnection.trigger('previousSlide');
        expect(store.getActions()).toContainEqual(goToPreviousSlide());
    });
});

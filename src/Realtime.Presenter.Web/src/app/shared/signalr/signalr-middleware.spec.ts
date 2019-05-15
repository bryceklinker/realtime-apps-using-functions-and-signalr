import {MockStore} from "redux-mock-store";

import {AppState} from "../app-state";
import {configureMockStore} from "../../../testing/configure-mock-store";
import {createSignalRMiddleware} from "./signalr-middleware";
import {loadCredentialsSuccess} from "../actions";
import {Credentials} from "../models";
import {FakeHubConnection} from "../../../testing/create-fake-hub-connection";
import {goToNextSlide, goToPreviousSlide} from "../../slides/actions";

describe('signalRMiddleware', () => {
    let credentials: Credentials;
    let store: MockStore<AppState>;
    let hubConnection: FakeHubConnection;
    let createHubConnection: jest.Mock;

    beforeEach(() => {
        hubConnection = new FakeHubConnection();
        createHubConnection = jest.fn().mockReturnValue(hubConnection);
        credentials = {
            signalRToken: 'this-is-my-token',
            signalRHubUrl: 'https://signalr.com'
        };

        store = configureMockStore([createSignalRMiddleware(createHubConnection)]);
    });

    it('should create signalr hub', () => {
        store.dispatch(loadCredentialsSuccess(credentials));
        expect(createHubConnection).toHaveBeenCalledWith(credentials);
    });

    it('should start signalr connection', () => {
        store.dispatch(loadCredentialsSuccess(credentials));
        expect(hubConnection.start).toHaveBeenCalled();
    });

    it('should dispatch next slide action', () => {
        store.dispatch(loadCredentialsSuccess(credentials));
        hubConnection.trigger('nextSlide');
        expect(store.getActions()).toContainEqual(goToNextSlide());
    });

    it('should dispatch previous slide action', () => {
        store.dispatch(loadCredentialsSuccess(credentials));
        hubConnection.trigger('previousSlide');
        expect(store.getActions()).toContainEqual(goToPreviousSlide());
    });
});

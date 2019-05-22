import {createTestingEpic, TestingEpic} from "../../../testing/create-testing-epic";
import {initialize, SharedActionTypes} from "../actions";
import {PayloadAction} from "typesafe-actions";
import {Credentials} from "../models";
import {settingsUpdated} from "../../settings/actions";
import {createRootReducer} from "../root-reducer";
import {createMemoryHistory} from "history";
import {createState} from "../../../testing/create-state";

const BASE_URL = 'https://hello.com';
describe('credentialsEpic', () => {
    let testingEpic: TestingEpic;

    beforeEach(() => {
        testingEpic = createTestingEpic(null, settingsUpdated({baseUrl: BASE_URL}));
    });

    it('should notify credentials were loaded successfully', done => {
        const credentials: Credentials = {
            signalRUrl: 'https://signalr.com/clients?hubName=something',
            signalRToken: 'this-is-a-token'
        };
        fetchMock.mockResponse(JSON.stringify(credentials));

        testingEpic.onAction(SharedActionTypes.LOAD_CREDENTIALS_SUCCESS)
            .subscribe((action: PayloadAction<string, Credentials>) => {
                expect(action.payload.signalRUrl).toEqual(credentials.signalRUrl);
                expect(action.payload.signalRToken).toEqual(credentials.signalRToken);
                done();
            });
        testingEpic.next(initialize());
    });

    it('should notify credentials failed to load', done => {
        fetchMock.mockReject(new Error('This is not a good response'));

        testingEpic.onAction(SharedActionTypes.LOAD_CREDENTIALS_FAILED)
            .subscribe((action: PayloadAction<string, any>) => {
                expect(action.payload.message).toEqual('This is not a good response');
                done();
            });
        testingEpic.next(initialize());
    });

    it('should get credentials from base url', done => {
        fetchMock.mockResponse(JSON.stringify({}));

        testingEpic.onAction(SharedActionTypes.LOAD_CREDENTIALS_SUCCESS)
            .subscribe(() => {
                expect(fetchMock.mock.calls[0][0]).toEqual(`${BASE_URL}/api/credentials`);
                done();
            });
        testingEpic.next(initialize());
    });

    it('should get credentials from new base url', done => {
        fetchMock.mockResponse(JSON.stringify({signalRUrl: 'https://signalr.com'}));

        testingEpic.onAction(SharedActionTypes.LOAD_CREDENTIALS_SUCCESS)
            .subscribe(() => {
                expect(fetchMock.mock.calls[3][0]).toEqual('https://one.com/api/credentials');
                done();
            });

        const state = createState(settingsUpdated({baseUrl: 'https://one.com'}));
        testingEpic.state$.next(state);
        testingEpic.next(settingsUpdated({baseUrl: 'https://one.com'}));
    })
});

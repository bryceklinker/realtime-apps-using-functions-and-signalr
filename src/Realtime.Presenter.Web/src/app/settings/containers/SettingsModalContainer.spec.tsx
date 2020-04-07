import {render, fireEvent} from "@testing-library/react";
import * as React from "react";
import {Provider} from "react-redux";

import {configureMockStore} from "../../../testing/configure-mock-store";
import {SettingsModalContainer} from "./SettingsModalContainer";
import {closeSettings, openSettings, settingsUpdated} from "../actions";
import {AppState} from "../../shared/app-state";
import {MockStore} from "redux-mock-store";
import {SettingsModel} from "../models";
import {selectSettingsBaseUrl} from "../reducers/settings-reducer";

describe('SettingsModalContainer', () => {
    it('should have open settings', async () => {
        const {getByTestId} = renderContainer(configureMockStore([], openSettings()));
        
        expect(getByTestId('settings-modal')).toBeVisible();
    });

    it('should not have open settings', async () => {
        const {queryAllByTestId} = renderContainer(configureMockStore([], openSettings(), closeSettings()));
        expect(queryAllByTestId('settings-modal')).toHaveLength(0);
    });

    it('should dispatch close settings', () => {
        const store = configureMockStore([], openSettings());

        const {getByTestId} = renderContainer(store);
        fireEvent.click(getByTestId('cancel-settings'));

        expect(store.getActions()).toContainEqual(closeSettings());
    });

    it('should dispatch close settings after save', () => {
        const store = configureMockStore([], openSettings());

        const {getByTestId} = renderContainer(store);
        fireEvent.click(getByTestId('save-settings'));

        expect(store.getActions()).toContainEqual(closeSettings());
    });

    it('should dispatch settings updated after save', () => {
        const store = configureMockStore([], settingsUpdated({baseUrl: 'http://localhost:7071'}), openSettings());

        const {getByTestId} = renderContainer(store);
        fireEvent.click(getByTestId('save-settings'));

        const expected: SettingsModel = {
            baseUrl: selectSettingsBaseUrl(store.getState())
        };
        expect(store.getActions()).toContainEqual(settingsUpdated(expected));
    });

    function renderContainer(store: MockStore<AppState>) {
        return render(
            <Provider store={store}>
                <SettingsModalContainer/>
            </Provider>
        )
    }
});

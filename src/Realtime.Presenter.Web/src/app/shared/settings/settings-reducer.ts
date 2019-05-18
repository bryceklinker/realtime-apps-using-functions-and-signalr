import {Action} from "redux";
import {initialize} from "../actions";
import {SettingsActionTypes} from "./actions";
import {PayloadAction} from "typesafe-actions";
import {AppState} from "../app-state";
import {createSelector} from "reselect";

export interface SettingsState {
    baseUrl: string;
}

const initialState: SettingsState = {
    baseUrl: 'http://localhost:7071'
}

export function settingsReducer(state: SettingsState = initialState, action: Action = initialize()) {
    switch (action.type) {
        case SettingsActionTypes.UPDATE_BASE_URL:
            return {...state, baseUrl: (<PayloadAction<string, string>>action).payload };
        default:
            return state;
    }
}

function selectSettings(state: AppState) {
    return state.settings;
}

export const selectSettingsBaseUrl = createSelector(selectSettings, s => s.baseUrl);

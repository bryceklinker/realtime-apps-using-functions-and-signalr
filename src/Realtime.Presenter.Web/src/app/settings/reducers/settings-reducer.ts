import {Action} from "redux";
import {initialize} from "../../shared/actions";
import {SettingsActionTypes} from "../actions";
import {PayloadAction} from "typesafe-actions";
import {AppState} from "../../shared/app-state";
import {createSelector} from "reselect";
import {SettingsModel} from "../models";

export interface SettingsState {
    settings: SettingsModel;
    isOpen: boolean;
}

const initialState: SettingsState = {
    settings: {
        baseUrl: 'https://realtime-apps-presentation-func.azurewebsites.net'
    },
    isOpen: false
}

export function settingsReducer(state: SettingsState = initialState, action: Action = initialize()) {
    switch (action.type) {
        case SettingsActionTypes.UPDATED:
            return {
                ...state,
                settings: {
                    ...state.settings,
                    ...(<PayloadAction<string, SettingsModel>>action).payload
                }
            };
        case SettingsActionTypes.OPEN:
            return {...state, isOpen: true};
        case SettingsActionTypes.CLOSE:
            return {...state, isOpen: false};
        default:
            return state;
    }
}

function selectSettingsState(state: AppState) {
    return state.settings;
}

export const selectSettingsBaseUrl = createSelector(selectSettingsState, s => s.settings.baseUrl);
export const selectSettingsIsOpen = createSelector(selectSettingsState, s => s.isOpen);
export const selectSettings = createSelector(selectSettingsState, s => s.settings);

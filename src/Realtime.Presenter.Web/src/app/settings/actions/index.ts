import {action} from "typesafe-actions";
import {SettingsModel} from "../models";

export const SettingsActionTypes = {
    OPEN: '[Settings] Open',
    CLOSE: '[Settings] Close',
    UPDATED: '[Settings] Updated'
};

export function openSettings() {
    return action(SettingsActionTypes.OPEN);
}

export function closeSettings() {
    return action(SettingsActionTypes.CLOSE);
}

export function settingsUpdated(model: SettingsModel) {
    return action(SettingsActionTypes.UPDATED, model);
}

import {action} from "typesafe-actions";

export const SettingsActionTypes = {
    UPDATE_BASE_URL: '[Settings] Update Base Url'
};

export function settingsUpdateBaseUrl(baseUrl: string) {
    return action(SettingsActionTypes.UPDATE_BASE_URL, baseUrl);
}

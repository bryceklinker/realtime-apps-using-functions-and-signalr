import {HubConnectionBuilder} from "@aspnet/signalr";
import {SettingsModel} from '../../settings/models';

export function createHubConnection(settings: SettingsModel) {
    return new HubConnectionBuilder()
        .withUrl(`${settings.baseUrl}/api`)
        .build();
}

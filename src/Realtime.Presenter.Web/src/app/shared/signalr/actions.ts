import {action} from "typesafe-actions";

export const SignalRActionTypes = {
    CONNECTED: '[SignalR] Connected',
    DISCONNECTED: '[SignalR] Disconnected',
};

export function signalRConnected() {
    return action(SignalRActionTypes.CONNECTED);
}

export function signalRDisconnected() {
    return action(SignalRActionTypes.DISCONNECTED);
}

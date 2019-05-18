import {Credentials} from "../models";
import {HubConnectionBuilder} from "@aspnet/signalr";

export function createHubConnection(credentials: Credentials) {
    return new HubConnectionBuilder()
        .withUrl(credentials.signalRUrl, {
            accessTokenFactory: () => credentials.signalRToken
        })
        .build();
}

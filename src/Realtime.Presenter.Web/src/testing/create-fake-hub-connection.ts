import {HubConnectionState} from "@aspnet/signalr";

export class FakeHubConnection {
    private readonly _handlers: { [key: string]: ((...args: any[]) => void)[] };

    state: HubConnectionState;

    constructor() {
        spyOn(this, 'start').and.callThrough();
        spyOn(this, 'stop').and.callThrough();
        spyOn(this, 'on').and.callThrough();

        this._handlers = {};
        this.state = HubConnectionState.Disconnected;
    }

    start() {
        this.state = HubConnectionState.Connected;
        return Promise.resolve();
    }

    stop() {
        this.state = HubConnectionState.Disconnected;
        return Promise.resolve();
    }

    on(methodName: string, newMethod: (...args: any[]) => void): void {
        let handlers = this._handlers[methodName];
        if (!handlers) {
            handlers = this._handlers[methodName] = []
        }
        handlers.push(newMethod);
    }

    trigger(methodName: string, ...args: any[]) {
        let handlers = this._handlers[methodName];
        if (!handlers) {
            return;
        }

        handlers.forEach(h => h(args));
    }
}

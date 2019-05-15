import {Action} from "redux";
import createMockStore, {MockStore} from 'redux-mock-store';
import {AppState} from "../app/shared/app-state";
import {createState} from "./create-state";

export function configureMockStore(...actions: Action[]): MockStore<AppState> {
    const state = createState(...actions);
    return createMockStore<AppState>([])(state);
}

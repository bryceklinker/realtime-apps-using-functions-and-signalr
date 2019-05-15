import {createMemoryHistory} from "history";
import {Action} from "redux";
import {AppState} from "../app/shared/app-state";
import {createRootReducer} from "../app/shared/root-reducer";
import {initialize} from "../app/shared/actions";

export function createState(...actions: Action[]) : AppState {
    const reducer = createRootReducer(createMemoryHistory());
    return actions.reduce((state, action) => reducer(state, action), reducer(undefined, initialize()));
}

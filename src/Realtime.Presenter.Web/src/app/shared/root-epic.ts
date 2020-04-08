import {combineEpics} from "redux-observable";
import {Action} from "redux";
import {AppState} from "./app-state";
import {EpicDependencies} from "./epic-dependencies";

export function createRootEpic() {
    return combineEpics<Action, Action, AppState, EpicDependencies>(
    );
}

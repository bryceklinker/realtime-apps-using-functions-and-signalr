import {combineEpics} from "redux-observable";
import {credentialsEpic} from "./epics/credentials-epic";

export function createRootEpic() {
    return combineEpics(credentialsEpic);
}

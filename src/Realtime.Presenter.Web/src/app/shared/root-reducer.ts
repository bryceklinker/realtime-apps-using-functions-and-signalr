import {combineReducers} from "redux";
import {History} from 'history';
import {connectRouter} from 'connected-react-router';
import {AppState} from "./app-state";
import {slidesReducer} from "../slides/reducers/slides-reducer";
import {settingsReducer} from "../settings/reducers/settings-reducer";

export function createRootReducer(history: History) {
    return combineReducers<AppState>({
        router: connectRouter(history),
        slides: slidesReducer,
        settings: settingsReducer
    })
}

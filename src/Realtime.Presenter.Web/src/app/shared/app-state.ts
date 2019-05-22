import {RouterState} from "connected-react-router";
import {SlidesState} from "../slides/reducers/slides-reducer";
import {SettingsState} from "../settings/reducers/settings-reducer";

export interface AppState {
    router: RouterState,
    slides: SlidesState,
    settings: SettingsState,
}

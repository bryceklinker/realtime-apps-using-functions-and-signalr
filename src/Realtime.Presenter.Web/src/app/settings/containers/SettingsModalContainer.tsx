import {connect} from "react-redux";
import {SettingsModal} from "../components/SettingsModal";
import {selectSettings, selectSettingsIsOpen} from "../reducers/settings-reducer";
import {AnyAction, Dispatch} from "redux";
import {closeSettings, settingsUpdated} from "../actions";
import {SettingsModel} from "../models";

function mapStateToProps(state) {
    return {
        isOpen: selectSettingsIsOpen(state),
        settings: selectSettings(state)
    }
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        onSettingsClosed: () => dispatch(closeSettings()),
        onSettingsUpdated: (settings: SettingsModel) => {
            dispatch(settingsUpdated(settings));
            dispatch(closeSettings());
        }
    }
}

export const SettingsModalContainer = connect(mapStateToProps, mapDispatchToProps)(SettingsModal);

import {connect} from "react-redux";
import {AppHeader} from "../components/AppHeader";
import {AnyAction, Dispatch} from "redux";
import {openSettings} from "../../settings/actions";

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        onOpenSettings: () => dispatch(openSettings())
    }
}

export const AppHeaderContainer = connect(null, mapDispatchToProps)(AppHeader);

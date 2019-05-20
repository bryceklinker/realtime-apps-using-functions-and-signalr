import {connect} from "react-redux";

import {SlidePresenter} from "../components/SlidePresenter";
import {AppState} from "../../shared/app-state";
import {selectCurrentSlide} from "../reducers/slides-reducer";
import {AnyAction, Dispatch} from "redux";
import {goToNextSlide, goToPreviousSlide} from "../actions";

function mapStateToProps(state: AppState) {
    return {
        slide: selectCurrentSlide(state)
    };
}

function mapDispatchToProps(dispatch: Dispatch<AnyAction>) {
    return {
        onNextSlide: () => dispatch(goToNextSlide()),
        onPreviousSlide: () => dispatch(goToPreviousSlide())
    }
}
export const SlidePresenterContainer = connect(mapStateToProps, mapDispatchToProps)(SlidePresenter);

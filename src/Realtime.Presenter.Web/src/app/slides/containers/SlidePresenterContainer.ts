import {connect} from "react-redux";

import {SlidePresenter} from "../components/SlidePresenter";
import {AppState} from "../../shared/app-state";
import {selectCurrentSlide} from "../reducers/slides-reducer";

function mapStateToProps(state: AppState) {
    return {
        slide: selectCurrentSlide(state)
    };
}
export const SlidePresenterContainer = connect(mapStateToProps, null)(SlidePresenter);

import {Action} from "typesafe-actions";
import { createSelector } from "reselect";
import {initialize} from "../../shared/actions";
import {SlidesActionTypes} from "../actions";
import {AppState} from "../../shared/app-state";
import {OpeningSlide} from "../components/OpeningSlide";

export interface SlidesState {
    currentSlideIndex: number;
    slides: any[];
}

const initialState: SlidesState = {
    currentSlideIndex: 0,
    slides: [
        OpeningSlide
    ]
};

export function slidesReducer(state: SlidesState = initialState, action: Action = initialize()) {
    switch (action.type) {
        case SlidesActionTypes.NEXT:
            return {...state, currentSlideIndex: state.currentSlideIndex + 1};
        case SlidesActionTypes.PREVIOUS:
            return {...state, currentSlideIndex: state.currentSlideIndex - 1};
        default:
            return state;
    }
}

function selectSlides(state: AppState): SlidesState {
    return state.slides;
}

export const selectCurrentSlide = createSelector(selectSlides, s => s.slides[s.currentSlideIndex]);

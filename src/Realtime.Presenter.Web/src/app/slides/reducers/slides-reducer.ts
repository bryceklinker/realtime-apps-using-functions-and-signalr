import {Action} from "typesafe-actions";
import { createSelector } from "reselect";
import {initialize} from "../../shared/actions";
import {SlidesActionTypes} from "../actions";
import {AppState} from "../../shared/app-state";
import {OpeningSlide} from "../components/slides/OpeningSlide";
import {AgendaSlide} from "../components/slides/AgendaSlide";
import {WhatIsAzureFunctionsSlide} from "../components/slides/WhatIsAzureFunctionsSlide";
import {AzureFunctionsV1VsV2} from "../components/slides/AzureFunctionsV1VsV2";

export interface SlidesState {
    currentSlideIndex: number;
    slides: any[];
}

const initialState: SlidesState = {
    currentSlideIndex: 0,
    slides: [
        OpeningSlide,
        AgendaSlide,
        WhatIsAzureFunctionsSlide,
        AzureFunctionsV1VsV2
    ]
};

function canGoToNextSlide(state: SlidesState) {
    return state.currentSlideIndex < state.slides.length - 1;
}

function incrementCurrentSlide(state: SlidesState) {
    return state.currentSlideIndex + 1;
}

function canGoToPreviousSlide(state: SlidesState) {
    return state.currentSlideIndex > 0;
}

function decrementCurrentSlide(state: SlidesState) {
    return state.currentSlideIndex - 1;
}

export function slidesReducer(state: SlidesState = initialState, action: Action = initialize()) {
    switch (action.type) {
        case SlidesActionTypes.NEXT:
            return {...state, currentSlideIndex: canGoToNextSlide(state) ? incrementCurrentSlide(state) : state.currentSlideIndex};
        case SlidesActionTypes.PREVIOUS:
            return {...state, currentSlideIndex: canGoToPreviousSlide(state) ? decrementCurrentSlide(state) : 0};
        default:
            return state;
    }
}

function selectSlides(state: AppState): SlidesState {
    return state.slides;
}

export const selectCurrentSlide = createSelector(selectSlides, s => s.slides[s.currentSlideIndex]);

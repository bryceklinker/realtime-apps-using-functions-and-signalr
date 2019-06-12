import {Action} from "typesafe-actions";
import { createSelector } from "reselect";
import {initialize} from "../../shared/actions";
import {SlidesActionTypes} from "../actions";
import {AppState} from "../../shared/app-state";
import {OpeningSlide} from "../components/slides/OpeningSlide";
import {AgendaSlide} from "../components/slides/AgendaSlide";
import {WhatIsAzureFunctionsSlide} from "../components/slides/WhatIsAzureFunctionsSlide";
import {AzureFunctionsV1VsV2Slide} from "../components/slides/AzureFunctionsV1VsV2Slide";
import {ReferencesSlide} from "../components/slides/ReferencesSlide";
import {WhatIsAzureSignalRSlide} from "../components/slides/WhatIsAzureSignalRSlide";
import {WhyShouldYouCareSlide} from "../components/slides/WhyShouldYouCareSlide";
import {ThankYouSlide} from "../components/slides/ThankYouSlide";
import {AzureSignalRServiceOptions} from "../components/slides/AzureSignalRServiceOptions";
import {QuestionsSlide} from "../components/slides/QuestionsSlide";
import {FeedbackSlide } from '../components/slides/FeedbackSlide';
import {CodeSlide} from "../components/slides/CodeSlide";
import {WhatAreRealtimeApplicationsSlide} from "../components/slides/WhatAreRealtimeApplicationsSlide";
import {RealtimeApplicationExamplesSlide} from "../components/slides/RealtimeApplicationExamplesSlide";
import {PresentationArchitectureSlide} from "../components/slides/PresentationArchitecture";

export interface SlidesState {
    currentSlideIndex: number;
    slides: any[];
}

const initialState: SlidesState = {
    currentSlideIndex: 0,
    slides: [
        OpeningSlide,
        AgendaSlide,
        WhatAreRealtimeApplicationsSlide,
        RealtimeApplicationExamplesSlide,
        WhyShouldYouCareSlide,
        WhatIsAzureFunctionsSlide,
        AzureFunctionsV1VsV2Slide,
        WhatIsAzureSignalRSlide,
        AzureSignalRServiceOptions,
        PresentationArchitectureSlide,
        CodeSlide,
        QuestionsSlide,
        FeedbackSlide,
        ThankYouSlide,
        ReferencesSlide
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

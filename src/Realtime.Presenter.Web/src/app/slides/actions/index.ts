import {action} from "typesafe-actions";

export const SlidesActionTypes = {
    NEXT: '[Slides] Go To Next',
    PREVIOUS: '[Slides] Go To Previous'
};

export function goToNextSlide() {
    return action(SlidesActionTypes.NEXT);
}

export function goToPreviousSlide() {
    return action(SlidesActionTypes.PREVIOUS);
}

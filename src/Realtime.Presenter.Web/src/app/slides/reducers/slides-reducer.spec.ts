import {slidesReducer} from "./slides-reducer";
import {goToNextSlide, goToPreviousSlide} from "../actions";

describe('slidesReducer', () => {
    it('should be on first slide', () => {
        const state = slidesReducer();
        expect(state.currentSlideIndex).toEqual(0);
    });

    it('should go to next slide', () => {
        let state = slidesReducer();
        state = slidesReducer(state, goToNextSlide());
        expect(state.currentSlideIndex).toEqual(1);
    });

    it('should go to previous slide', () => {
        let state = slidesReducer();
        state = slidesReducer(state, goToNextSlide());
        state = slidesReducer(state, goToPreviousSlide());
        expect(state.currentSlideIndex).toEqual(0);
    })

    it('should have slides ordered and ready', () => {
        let state = slidesReducer();
        expect(state.slides).toHaveLength(1);
    })
});

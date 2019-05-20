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
    });

    it('should have slides ordered and ready', () => {
        const state = slidesReducer();
        expect(state.slides).toHaveLength(4);
    });

    it('should stay on current slide if current is at the start', () => {
        let state = slidesReducer();
        state = slidesReducer(state, goToPreviousSlide());
        expect(state.currentSlideIndex).toEqual(0);
    });

    it('should not go past the last slide', () => {
        let state = slidesReducer();
        const slides = state.slides;
        slides.forEach(() => state = slidesReducer(state, goToNextSlide()));
        state = slidesReducer(state, goToNextSlide());

        expect(state.currentSlideIndex).toEqual(state.slides.length - 1);
    })
});

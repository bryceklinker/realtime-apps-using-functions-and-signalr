import React from 'react';
import {render, fireEvent} from 'react-testing-library';
import {SlideToolbar} from "./SlideToolbar";


describe('SlideToolbar', () => {
    let nextSlide, previousSlide;

    beforeEach(() => {
        nextSlide = jest.fn();
        previousSlide = jest.fn();
    });

    it('should call next slide', () => {
        const {getByTestId} = render(<SlideToolbar onNextSlide={nextSlide} onPreviousSlide={previousSlide}/>);
        fireEvent.click(getByTestId('next-slide'));
        expect(nextSlide).toHaveBeenCalled();
    });
});

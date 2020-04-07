import React from 'react';
import {render, fireEvent} from '@testing-library/react';
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

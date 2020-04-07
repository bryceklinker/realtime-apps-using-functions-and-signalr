import React from "react";
import {render} from "@testing-library/react";

import {SlidePresenter} from "./SlidePresenter";

describe('SlidePresenter', () => {
    it('should show slide', async () => {
        const { getByText } = render(<SlidePresenter slide={FakeComponent} onPreviousSlide={jest.fn()} onNextSlide={jest.fn()} />);
        
        expect(getByText('This is data')).toBeVisible();
    });
});

function FakeComponent() {
    return <span>This is data</span>;
}

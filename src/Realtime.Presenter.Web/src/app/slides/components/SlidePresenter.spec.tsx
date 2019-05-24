import React from "react";
import {render, waitForElement} from "react-testing-library";

import {SlidePresenter} from "./SlidePresenter";

describe('SlidePresenter', () => {
    it('should show slide', async () => {
        const { getByText } = render(<SlidePresenter slide={FakeComponent} onPreviousSlide={jest.fn()} onNextSlide={jest.fn()} />);
        await waitForElement(() => getByText('This is data'));
    });
});

function FakeComponent() {
    return <span>This is data</span>;
}

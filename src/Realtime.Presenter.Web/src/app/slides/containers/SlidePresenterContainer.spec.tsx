import React from "react";
import { render, waitForElement } from 'react-testing-library';

import {SlidePresenterContainer} from "./SlidePresenterContainer";
import {Provider} from "react-redux";
import {configureMockStore} from "../../../testing/configure-mock-store";

describe('SlidePresenterContainer', () => {
    it('should show the current slide', async () => {
        const { getByText } = renderContainer();
        await waitForElement(() => getByText(/Building/));
    });

    function renderContainer() {
        return render(
            <Provider store={configureMockStore()}>
                <SlidePresenterContainer />
            </Provider>
        )
    }
});

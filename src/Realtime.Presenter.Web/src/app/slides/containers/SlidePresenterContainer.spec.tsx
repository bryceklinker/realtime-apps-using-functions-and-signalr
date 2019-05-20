import React from "react";
import {render, waitForElement, fireEvent} from 'react-testing-library';

import {SlidePresenterContainer} from "./SlidePresenterContainer";
import {Provider} from "react-redux";
import {configureMockStore} from "../../../testing/configure-mock-store";
import {AppState} from "../../shared/app-state";
import {MockStore} from "redux-mock-store";
import {goToNextSlide} from "../actions";

describe('SlidePresenterContainer', () => {
    let store: MockStore<AppState>;

    beforeEach(() => {
        store = configureMockStore();
    });

    it('should show the current slide', async () => {
        const {getByText} = renderContainer();
        await waitForElement(() => getByText(/Welcome/));
    });

    it('should go to next slide', async () => {
        const {getByTestId} = renderContainer();
        fireEvent.click(getByTestId('next-slide'));
        expect(store.getActions()).toContainEqual(goToNextSlide());
    });

    function renderContainer() {
        return render(
            <Provider store={store}>
                <SlidePresenterContainer/>
            </Provider>
        )
    }
});

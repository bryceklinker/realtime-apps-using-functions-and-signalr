import React from "react";
import {render, waitForElement} from 'react-testing-library';
import {createMemoryHistory} from 'history';

import {App} from "./App";
import {createTheme} from "./shared/create-theme";
import {configureStore} from "./shared/configure-store";

describe('App', () => {
    it('should show title header', async () => {
        const history = createMemoryHistory();
        const {getAllByText} = render(<App theme={createTheme()} store={configureStore(history)} history={history}/>);
        await waitForElement(() => getAllByText('Building Realtime Apps with Azure Functions and SignalR'));
    });
});

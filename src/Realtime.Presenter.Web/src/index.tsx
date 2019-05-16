import React from 'react';
import {render} from 'react-dom';
import {createBrowserHistory} from 'history';
import {App} from "./app/App";
import {createTheme} from "./app/shared/create-theme";
import {configureStore} from "./app/shared/configure-store";

import './index.scss';
const history = createBrowserHistory();
const store = configureStore(history);
const theme = createTheme();
render(
    <App theme={theme} store={store} history={history}/>,
    document.getElementById('root')
);

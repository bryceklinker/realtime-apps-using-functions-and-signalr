import React from "react";
import {Provider} from 'react-redux';
import {MuiThemeProvider} from "@material-ui/core";
import {ConnectedRouter} from "connected-react-router";

import {Shell} from "./Shell";

export function App({theme, store, history}) {
    return (
        <Provider store={store}>
            <ConnectedRouter history={history}>
                <MuiThemeProvider theme={theme}>
                    <Shell />
                </MuiThemeProvider>
            </ConnectedRouter>
        </Provider>
    )
}

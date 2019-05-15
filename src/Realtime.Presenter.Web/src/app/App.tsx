import React from "react";
import {Provider} from 'react-redux';
import {Grid, MuiThemeProvider} from "@material-ui/core";
import {ConnectedRouter} from "connected-react-router";
import {Route, Switch} from 'react-router';

import {AppHeader} from "./header/components/AppHeader";
import {SlidePresenterContainer} from "./slides/containers/SlidePresenterContainer";

export function App({theme, store, history}) {
    return (
        <Provider store={store}>
            <ConnectedRouter history={history}>
                <MuiThemeProvider theme={theme}>
                    <Grid container>
                        <Grid item xs={12}>
                            <AppHeader/>
                        </Grid>
                        <Grid item xs={12}>
                            <Switch>
                                <Route path={'/'} component={SlidePresenterContainer} />
                            </Switch>
                        </Grid>
                    </Grid>
                </MuiThemeProvider>
            </ConnectedRouter>
        </Provider>
    )
}

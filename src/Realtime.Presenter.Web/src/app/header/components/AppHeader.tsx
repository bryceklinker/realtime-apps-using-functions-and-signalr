import React from "react";
import {AppBar, createStyles, Grid, Theme, Toolbar, Typography, WithStyles, withStyles} from "@material-ui/core";

interface Props extends WithStyles<typeof styles> {

}

function AppHeaderComponent({ classes }: Props) {
    return (
        <AppBar position={'static'}>
            <Toolbar>
                <Typography variant="h3" gutterBottom>
                    Building Realtime Apps with Azure Functions and SignalR
                </Typography>
            </Toolbar>
        </AppBar>
    );
}

const styles = createStyles({

});

export const AppHeader = withStyles(styles)(AppHeaderComponent);

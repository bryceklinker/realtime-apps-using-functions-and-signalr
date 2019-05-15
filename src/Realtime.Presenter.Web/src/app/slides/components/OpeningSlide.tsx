import React from "react";
import {Grid, Typography} from "@material-ui/core";

export function OpeningSlide() {
    return (
        <Grid container>
            <Grid item xs={12}>
                <Typography variant={'h3'}>
                    Building Realtime Apps with Azure Functions and SignalR
                </Typography>
            </Grid>
        </Grid>
    );
}

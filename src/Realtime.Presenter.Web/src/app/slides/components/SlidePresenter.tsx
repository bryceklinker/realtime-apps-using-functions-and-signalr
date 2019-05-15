import React from "react";
import {Grid} from "@material-ui/core";

export function SlidePresenter({ slide:SlideComponent }) {
    return (
        <Grid container>
            <Grid item xs={12}>
                <SlideComponent />
            </Grid>
        </Grid>
    )
}

import React from "react";
import {createStyles, Grid, Typography, WithStyles, withStyles} from "@material-ui/core";

interface Props extends WithStyles<typeof styles> {

}

function OpeningSlideComponent({ classes }: Props) {
    return (
        <Grid container>
            <Grid item xs={12}>
                <Typography variant={'h3'}>
                    Welcome .NET@Noon
                </Typography>
            </Grid>
            <Grid item xs={12} className={classes.spacer}>
            </Grid>
            <Grid item xs={12}>
                <Typography variant={'h3'}>
                    Thanks to Industry X.O for hosting.
                </Typography>
            </Grid>
        </Grid>
    );
}

const styles = createStyles({
    spacer: {
        flexGrow: 1
    }
});

export const OpeningSlide = withStyles(styles)(OpeningSlideComponent);

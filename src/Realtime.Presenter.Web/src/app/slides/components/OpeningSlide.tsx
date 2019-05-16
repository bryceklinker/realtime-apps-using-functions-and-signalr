import React from "react";
import {createStyles, Typography, WithStyles, withStyles} from "@material-ui/core";

interface Props extends WithStyles<typeof styles> {

}

function OpeningSlideComponent({ classes }: Props) {
    return (
        <div className={classes.root}>
            <div>
                <Typography variant={'h3'}>
                    Welcome .NET@Noon
                </Typography>
            </div>
            <div></div>
            <div className={classes.bottom}>
                <Typography variant={'h3'}>
                    Thanks to Industry X.O for hosting.
                </Typography>
            </div>
        </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: 'auto auto auto'
    },
    bottom: {
        alignContent: 'end'
    }
});

export const OpeningSlide = withStyles(styles)(OpeningSlideComponent);

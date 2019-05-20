import React from "react";
import {createStyles, Typography, WithStyles, withStyles} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";

const functionsImage = require('../../../../assets/azure-functions.png');
const signalrImage = require('../../../../assets/azure-signalr.svg');

interface Props extends WithStyles<typeof styles> {

}

function OpeningSlideComponent({ classes }: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Welcome to .NET@Noon'} />
            <div className={classes.images}>
                <div><img src={functionsImage} width={'50%'}/></div>
                <div><img src={signalrImage} width={'50%'}/></div>
            </div>
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
    },
    images: {
        gridTemplateColumns: 'auto auto'
    }
});

export const OpeningSlide = withStyles(styles)(OpeningSlideComponent);

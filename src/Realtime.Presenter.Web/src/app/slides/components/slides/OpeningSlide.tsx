import React from "react";
import {createStyles, Typography, WithStyles, withStyles} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";
// @ts-ignore
import functionsImage from '../../../../assets/azure-functions.png';
// @ts-ignore
import signalrImage from '../../../../assets/azure-signalr.svg';


interface Props extends WithStyles<typeof styles> {

}

function OpeningSlideComponent({ classes }: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Junto'} />
            <div className={classes.images}>
                <div><img className={classes.image} src={functionsImage} width={'50%'}/></div>
                <div><img className={classes.image} src={signalrImage} width={'50%'}/></div>
            </div>
        </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: '48px auto auto auto'
    },
    bottom: {
        alignContent: 'end'
    },
    images: {
        alignItems: 'center',
        gridTemplateColumns: '50% 50%'
    },
    image: {
        justifySelf: 'center'
    }
});

export const OpeningSlide = withStyles(styles)(OpeningSlideComponent);

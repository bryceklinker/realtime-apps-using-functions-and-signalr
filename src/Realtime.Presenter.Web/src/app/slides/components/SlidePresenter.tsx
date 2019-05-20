import React from "react";
import {createStyles, Grid, withStyles, WithStyles} from "@material-ui/core";
import {SlideToolbar} from "./SlideToolbar";

interface Props extends WithStyles<typeof styles> {
    slide: any;
    onNextSlide: () => void;
    onPreviousSlide: () => void;
}

function SlidePresenterComponent({ slide:SlideComponent, classes, onNextSlide, onPreviousSlide }) {
    return (
        <div className={classes.root}>
            <div>
                <SlideComponent />
            </div>
            <div className={classes.toolbar}>
                <SlideToolbar onNextSlide={onNextSlide} onPreviousSlide={onPreviousSlide}/>
            </div>
        </div>
    )
}

const styles = createStyles({
    root: {
        gridTemplateRows: 'auto 64px'
    },
    toolbar: {
        maxHeight: '64px'
    }
});

export const SlidePresenter = withStyles(styles)(SlidePresenterComponent);

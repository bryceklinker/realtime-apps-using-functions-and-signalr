import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {slideWithTitleStyle} from "./common/slide-style";
import {SlideTitle} from "./common/SlideTitle";

interface Props extends WithStyles<typeof styles> {

}

function CodeSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'You said there would be code...'} />
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle
});

export const CodeSlide = withStyles(styles)(CodeSlideComponent);

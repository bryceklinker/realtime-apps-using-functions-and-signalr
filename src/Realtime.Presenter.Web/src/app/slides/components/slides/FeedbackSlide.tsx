import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import { ThumbUpRounded, ThumbDownRounded } from '@material-ui/icons';
import green from '@material-ui/core/colors/green';

import {slideWithTitleStyle} from "./common/slide-style";
import {SlideTitle} from "./common/SlideTitle";

interface Props extends WithStyles<typeof styles> {

}

function FeedbackSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Feedback'}/>
            <div className={classes.content}>
                <ThumbUpRounded style={{'color': green.A700}} className={classes.icon}/>
                <ThumbDownRounded color={'error'} className={classes.icon}/>
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    content: {
        ...slideWithTitleStyle.content,
        justifyItems: 'center',
        alignItems: 'center',
    },
    icon: {
        fontSize: '210px',
    },

});

export const FeedbackSlide = withStyles(styles)(FeedbackSlideComponent);

import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
// @ts-ignore
import questions from '../../../../assets/questions.jpeg';

interface Props extends WithStyles<typeof styles> {

}

function QuestionsSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <img className={classes.image} src={questions} />
        </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: 'auto',
        justifyItems: 'center'
    },
    image: {
        height: '100%'
    }
});

export const QuestionsSlide = withStyles(styles)(QuestionsSlideComponent);

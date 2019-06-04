import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';

const thankyou = require('../../../../assets/thankyou.gif')

interface Props extends WithStyles<typeof styles> {

}

function ThankYouSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <div className={classes.content}>
                <img className={classes.image} src={thankyou} />
            </div>
        </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: 'auto'
    },
    content: {
        gridTemplateColumns: 'auto',
        alignItems: 'center',
        justifyItems: 'center'
    },
    image: {
        height: '100%'
    }
});

export const ThankYouSlide = withStyles(styles)(ThankYouSlideComponent);

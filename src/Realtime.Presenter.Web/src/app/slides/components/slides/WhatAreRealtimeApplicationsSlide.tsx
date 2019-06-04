import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from '@material-ui/core';
import {slideWithTitleStyle} from "./common/slide-style";
import {SlideTitle} from "./common/SlideTitle";

interface Props extends WithStyles<typeof styles> {

}

function WhatAreRealtimeApplicationsSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'What are Realtime Applications?'}/>
            <div className={classes.content}>
                <Typography variant={'h4'} className={classes.quote}>
                    A real-time application (RTA) is an application program that functions within a time frame that the
                    user senses as immediate or current
                </Typography>

                <a className={classes.referenceLink} href={'https://searchunifiedcommunications.techtarget.com/definition/real-time-application-RTA'}>
                    <Typography variant={'h5'} className={classes.reference}>
                        TechTarget
                    </Typography>
                </a>
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    content: {
        justifyItems: 'center',
        alignItems: 'center',
        textAlign: 'center',
        alignContent: 'center'
    },
    quote: {
        fontStyle: 'italic'
    },
    referenceLink: {
      textDecoration: 'none'
    },
    reference: {
        fontStyle: 'italic',
        color: '#999999'
    }
});

export const WhatAreRealtimeApplicationsSlide = withStyles(styles)(WhatAreRealtimeApplicationsSlideComponent);

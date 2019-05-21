import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {SlideTitle} from "./common/SlideTitle";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";

const signalRImage = require('../../../../assets/azure-signalr.svg');

interface Props extends WithStyles<typeof styles> {

}

function WhatIsAzureSignalRSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'What is Azure SignalR?'} />
            <div className={classes.content}>
                <div>
                    <BulletList>
                        <BulletListItem text={'A hosted version of ASP.NET Core SignalR'}/>
                        <BulletListItem text={'Highly scalable'} />
                    </BulletList>
                </div>
                <div>
                    <img src={signalRImage} className={classes.image} />
                </div>
            </div>
        </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: '48px auto'
    },
    content: {
        gridTemplateColumns: '50% 50%'
    },
    image: {
        maxHeight: '50%',
        maxWidth: '50%',
    }
});

export const WhatIsAzureSignalRSlide = withStyles(styles)(WhatIsAzureSignalRSlideComponent);

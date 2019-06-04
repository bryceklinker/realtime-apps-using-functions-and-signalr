import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {SlideTitle} from "./common/SlideTitle";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";
import {slideWithTitleStyle} from "./common/slide-style";

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
                        <BulletListItem text={'Recently support for ASP.NET SignalR has been added'}/>
                    </BulletList>
                </div>
                <div className={classes.images}>
                    <img src={signalRImage} className={classes.image} />
                </div>
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    images: {
      justifyItems: 'center',
      alignItems: 'center',
    },
    image: {
        maxHeight: '50%',
        maxWidth: '50%',
    }
});

export const WhatIsAzureSignalRSlide = withStyles(styles)(WhatIsAzureSignalRSlideComponent);

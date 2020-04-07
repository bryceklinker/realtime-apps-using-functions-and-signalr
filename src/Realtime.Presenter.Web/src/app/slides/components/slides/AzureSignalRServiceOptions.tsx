import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {SlideTitle} from "./common/SlideTitle";
import {slideWithTitleStyle} from "./common/slide-style";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";
// @ts-ignore
import choices from '../../../../assets/choices.png';

interface Props extends WithStyles<typeof styles> {

}

function AzureSignalRServiceOptionsComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Azure SignalR Service Options'} />
            <div className={classes.content}>
                <BulletList>
                    <BulletListItem text={'Free Tier'} />
                    <BulletListItem text={'Standard Tier (~$50 per mo)'} />
                    <BulletListItem text={'Serverless Mode'} />
                    <BulletListItem text={'REST API'} />
                </BulletList>
                <img className={classes.image} src={choices} />
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    image: {
        height: '90%'
    }
});

export const AzureSignalRServiceOptions = withStyles(styles)(AzureSignalRServiceOptionsComponent);

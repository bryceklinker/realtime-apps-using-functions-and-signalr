import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {SlideTitle} from "./common/SlideTitle";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";
import {slideWithTitleStyle} from "./common/slide-style";
// @ts-ignore
import whyImage from '../../../../assets/why.jpeg';

interface Props extends WithStyles<typeof styles> {

}

function WhyShouldYouCareSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Why Should You Care?'} />

            <div className={classes.content}>
                <div>
                    <BulletList>
                        <BulletListItem text={'Event based systems are becoming more common.'} />
                        <BulletListItem text={'Scaling is a difficult thing to get right'} />
                        <BulletListItem text={'Why pay for more compute than you need?'} />
                    </BulletList>
                </div>
                <div>
                    <img src={whyImage} className={classes.image}/>
                </div>
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    image: {
        height: '95%',
        width: '95%'
    }
});

export const WhyShouldYouCareSlide = withStyles(styles)(WhyShouldYouCareSlideComponent);

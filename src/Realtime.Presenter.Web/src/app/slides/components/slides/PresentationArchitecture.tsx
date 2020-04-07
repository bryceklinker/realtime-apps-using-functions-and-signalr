import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {slideWithTitleStyle} from './common/slide-style';
import {SlideTitle} from './common/SlideTitle';
// @ts-ignore
import architecture from '../../../../assets/architecture.png';

interface Props extends WithStyles<typeof styles> {

}

function PresentationArchitectureSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Presentation Architecture'}/>
            <div className={classes.content}>
                <img className={classes.img} src={architecture}/>
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    content: {
        gridTemplateRows: '100%',
        justifyItems: 'center'
    },
    img: {
        maxHeight: '100%'
    }
});

export const PresentationArchitectureSlide = withStyles(styles)(PresentationArchitectureSlideComponent);

import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from '@material-ui/core';
import {slideWithTitleStyle} from './common/slide-style';
import {SlideTitle} from './common/SlideTitle';
// @ts-ignore
import flightRadar from '../../../../assets/flight-radar.png';
// @ts-ignore
import jenkinsDashboard from '../../../../assets/jenkins-dashboard.png';
// @ts-ignore
import nest from '../../../../assets/nest.jpeg';

interface Props extends WithStyles<typeof styles> {

}

function RealtimeApplicationExamplesSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Realtime Application Examples'}/>
            <div className={classes.images}>
                <ImageExample title={'Flightradar24'} image={flightRadar} classes={classes}/>
                <ImageExample title={'Jenkins Dashboard'} image={jenkinsDashboard} classes={classes}/>
                <ImageExample title={'Nest'} image={nest} classes={classes}/>
            </div>
        </div>
    );
}

function ImageExample({title, image, classes}) {
    return (
        <div className={classes.imageContainer}>
            <img className={classes.image} src={image}/>
            <Typography className={classes.imageTitle} variant={'h5'}>{title}</Typography>
        </div>
    )
}

const styles = createStyles({
    ...slideWithTitleStyle,
    images: {
        gridTemplateColumns: '50% 50%',
        gridTemplateRows: '50% 50%'
    },
    imageContainer: {
        gridTemplateRows: 'auto 48px'
    },
    imageTitle: {
        color: '#999999'
    },
    image: {
        height: '100%'
    }
});

export const RealtimeApplicationExamplesSlide = withStyles(styles)(RealtimeApplicationExamplesSlideComponent);

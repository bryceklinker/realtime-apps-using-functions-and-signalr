import React from 'react';
import {AppBar, createStyles, IconButton, Toolbar, withStyles, WithStyles} from "@material-ui/core";
import { ArrowBack, ArrowForward } from '@material-ui/icons';

interface Props extends WithStyles<typeof styles> {
    onNextSlide: () => void;
    onPreviousSlide: () => void;
}

function SlideToolbarComponent({classes, onNextSlide, onPreviousSlide}: Props) {
    return (
        <div>
            <AppBar position={'static'}>
                <Toolbar className={classes.toolbar}>
                    <IconButton onClick={onPreviousSlide} data-testid={'previous-slide'}>
                        <ArrowBack />
                    </IconButton>
                    <div />
                    <IconButton onClick={onNextSlide} data-testid={'next-slide'}>
                        <ArrowForward />
                    </IconButton>
                </Toolbar>
            </AppBar>
        </div>
    )
}

const styles = createStyles({
    toolbar: {
        gridTemplateColumns: '48px auto 48px',
        display: 'grid',
    }
});

export const SlideToolbar = withStyles(styles)(SlideToolbarComponent);

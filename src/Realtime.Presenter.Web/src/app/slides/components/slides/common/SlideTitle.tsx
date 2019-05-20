import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from '@material-ui/core';

interface Props extends WithStyles<typeof styles> {
    title: string;
}

function SlideTitleComponent({classes, title}: Props) {
    return (
        <div>
            <Typography variant={'h3'}>{title}</Typography>
        </div>
    );
}

const styles = createStyles({

});

export const SlideTitle = withStyles(styles)(SlideTitleComponent);

import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';

interface Props extends WithStyles<typeof styles> {
    children: any[]
}

function BulletListComponent({classes, children}: Props) {
    return (
        <ul>
            {children}
        </ul>
    );
}

const styles = createStyles({

});

export const BulletList = withStyles(styles)(BulletListComponent);

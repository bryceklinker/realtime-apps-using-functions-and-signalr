import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';

interface Props extends WithStyles<typeof styles> {
    children: any | any[]
}

function BulletListComponent({classes, children}: Props) {
    return (
        <ul className={classes.list}>
            {children}
        </ul>
    );
}

const styles = createStyles({
    list: {
        fontSize: '45px'
    }
});

export const BulletList = withStyles(styles)(BulletListComponent);

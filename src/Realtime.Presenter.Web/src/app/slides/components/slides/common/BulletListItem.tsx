import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from '@material-ui/core';

interface Props extends WithStyles<typeof styles> {
    text: string;
    children?: any
}

function BulletListItemComponent({classes, text, children}: Props) {
    return (
        <li>
            <Typography variant={'h4'}>
                {text}
            </Typography>
            {children ? children : null}
        </li>
    );
}

const styles = createStyles({

});

export const BulletListItem = withStyles(styles)(BulletListItemComponent);

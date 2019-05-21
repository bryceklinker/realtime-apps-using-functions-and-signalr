import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from '@material-ui/core';

interface Props extends WithStyles<typeof styles> {
    href: string;
    text: string;
}

function BulletListItemLinkComponent({classes, href, text}: Props) {
    return (
        <li>
            <a href={href} target={'_blank'}>
                <Typography variant={'h4'}>
                    {text}
                </Typography>
            </a>
        </li>
    );
}

const styles = createStyles({});

export const BulletListItemLink = withStyles(styles)(BulletListItemLinkComponent);

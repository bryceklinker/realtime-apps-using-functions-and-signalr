import React from 'react';
import {withStyles, WithStyles, createStyles} from '@material-ui/core';
import {BulletList} from "./common/BulletList";
import {BulletListItemLink} from "./common/BulletListItemLink";

interface Props extends WithStyles<typeof styles> {

}

function ReferencesComponent({classes}: Props) {
    return (
        <div>
            <BulletList>
                <BulletListItemLink href={'https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings#supported-bindings'}
                                    text={'Supported Function Triggers'} />
                <BulletListItemLink href={'https://github.com/bryceklinker/realtime-apps-using-functions-and-signalr'}
                                    text={'GitHub Repo'} />
            </BulletList>
        </div>
    );
}

const styles = createStyles({

});

export const ReferencesSlide = withStyles(styles)(ReferencesComponent);

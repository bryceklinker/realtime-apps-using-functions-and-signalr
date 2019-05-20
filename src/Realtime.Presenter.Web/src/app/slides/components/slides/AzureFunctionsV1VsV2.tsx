import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";

interface Props extends WithStyles<typeof styles> {

}

function AzureFunctionsV1VsV2Component({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Azure Functions V1 vs V2'} />
        </div>
    )
}

const styles = createStyles({
    root: {
        gridTemplateRows: '48px auto'
    }
});

export const AzureFunctionsV1VsV2 = withStyles(styles)(AzureFunctionsV1VsV2Component);

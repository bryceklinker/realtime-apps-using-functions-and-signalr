import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";

interface Props extends WithStyles<typeof styles> {

}

function AzureFunctionsV1VsV2Component({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Azure Functions V1 vs V2'} />
            <div className={classes.content}>
                <div>
                    <BulletList>
                        <BulletListItem text={'V1'}>
                            <BulletList>
                                <BulletListItem text={'Required static class/methods'} />
                                <BulletListItem text={'Dependency injection unofficially supported'} />
                                <BulletListItem text={'Logger given to static method'} />
                            </BulletList>
                        </BulletListItem>
                        <BulletListItem text={'V2'}>
                            <BulletList>
                                <BulletListItem text={'Static class/method no longer required'} />
                                <BulletListItem text={'Dependency injection officially supported'} />
                                <BulletListItem text={'Logger can be injected'} />
                            </BulletList>
                        </BulletListItem>
                    </BulletList>
                </div>
                <div>

                </div>
            </div>
        </div>
    )
}

const styles = createStyles({
    root: {
        gridTemplateRows: '48px auto'
    },
    content: {
        gridTemplateColumns: '50% 50%'
    }
});

export const AzureFunctionsV1VsV2Slide = withStyles(styles)(AzureFunctionsV1VsV2Component);

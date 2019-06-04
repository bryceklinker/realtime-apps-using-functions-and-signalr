import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";
import {slideWithTitleStyle} from "./common/slide-style";

const improvements = require('../../../../assets/improvements.png');

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
                    <img className={classes.image} src={improvements} />
                </div>
            </div>
        </div>
    )
}

const styles = createStyles({
    ...slideWithTitleStyle,
    image: {
        width: '100%'
    }
});

export const AzureFunctionsV1VsV2Slide = withStyles(styles)(AzureFunctionsV1VsV2Component);

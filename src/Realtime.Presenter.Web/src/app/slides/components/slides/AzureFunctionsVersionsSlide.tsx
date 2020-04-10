import React from 'react';
import {withStyles, WithStyles, createStyles, Typography} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";
import {BulletList} from "./common/BulletList";
import {BulletListItem} from "./common/BulletListItem";
import {slideWithTitleStyle} from "./common/slide-style";

// @ts-ignore
import improvements from '../../../../assets/improvements.png'; 

interface Props extends WithStyles<typeof styles> {

}

function AzureFunctionsVersionsComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'Azure Functions V1 vs V2'} />
            <div className={classes.content}>
                <div>
                    <BulletList>
                        <BulletListItem text={'V1'}>
                            <BulletList>
                                <BulletListItem text={'Required static class/methods'} />
                                <BulletListItem text={'Logger given to static method'} />
                                <BulletListItem text={'.NET Framework 4.7.2'} />
                            </BulletList>
                        </BulletListItem>
                        <BulletListItem text={'V2'}>
                            <BulletList>
                                <BulletListItem text={'Static class/method no longer required'} />
                                <BulletListItem text={'Logger can be injected'} />
                                <BulletListItem text={'.NET Core 2.2'} />
                            </BulletList>
                        </BulletListItem>
                        <BulletListItem text={'V3'}>
                            <BulletList>
                                <BulletListItem text={'.NET Core 3.1'} />
                            </BulletList>
                        </BulletListItem>
                    </BulletList>
                </div>
                <div className={classes.images}>
                    <img className={classes.image} src={improvements} />
                </div>
            </div>
        </div>
    )
}

const styles = createStyles({
    ...slideWithTitleStyle,
    images: {
        alignItems: 'center'
    },
    image: {
        width: '100%'
    }
});

export const AzureFunctionsVersionsSlide = withStyles(styles)(AzureFunctionsVersionsComponent);

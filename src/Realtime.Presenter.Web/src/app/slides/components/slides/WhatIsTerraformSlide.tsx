import React from 'react';
import {createStyles, withStyles, WithStyles} from '@material-ui/core';
import {SlideTitle} from './common/SlideTitle';
import {BulletList} from './common/BulletList';
import {BulletListItem} from './common/BulletListItem';
import {slideWithTitleStyle} from './common/slide-style';
import {BulletListItemLink} from './common/BulletListItemLink';
// @ts-ignore
import terraformImage from '../../../../assets/terraform.png';

interface Props extends WithStyles<typeof styles> {

}

function WhatIsTerraformSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'What is Terraform?'} />
            <div className={classes.content}>
                <div>
                    <BulletList>
                        <BulletListItem text={'Infrastructure as Code'} />
                        <BulletListItem text={'Works with AWS, Azure, GCP, etc.'}>
                            <BulletList>
                                <BulletListItemLink text={'Providers'} 
                                                    href={'https://www.terraform.io/docs/providers/index.html'}/>
                            </BulletList>
                        </BulletListItem>
                    </BulletList>
                </div>
                <div className={classes.images}>
                    <img src={terraformImage} className={classes.image} />
                </div>
            </div>
        </div>
    );
}

const styles = createStyles({
    ...slideWithTitleStyle,
    images: {
        justifyItems: 'center',
        alignItems: 'center',
    },
    image: {
        maxHeight: '50%',
        maxWidth: '50%',
    }
});

export const WhatIsTerraformSlide = withStyles(styles)(WhatIsTerraformSlideComponent);
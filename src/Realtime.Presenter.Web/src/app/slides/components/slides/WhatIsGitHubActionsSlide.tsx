import React from 'react';
import {createStyles, withStyles, WithStyles} from '@material-ui/core';
import {SlideTitle} from './common/SlideTitle';
import {BulletList} from './common/BulletList';
import {BulletListItem} from './common/BulletListItem';
import {slideWithTitleStyle} from './common/slide-style';
// @ts-ignore
import gitHubActionsImage from '../../../../assets/github-actions.png';

interface Props extends WithStyles<typeof styles> {

}

function WhatIsGitHubActionsSlideComponent({classes}: Props) {
    return (
        <div className={classes.root}>
            <SlideTitle title={'What are GitHub Actions?'} />
            <div className={classes.content}>
                <div>
                    <BulletList>
                        <BulletListItem text={'Hosted CI/CD Solution'} />
                        <BulletListItem text={'Utilizes Azure DevOps Pipelines'} />
                    </BulletList>
                </div>
                <div className={classes.images}>
                    <img src={gitHubActionsImage} className={classes.image} />
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

export const WhatIsGitHubActionsSlide = withStyles(styles)(WhatIsGitHubActionsSlideComponent);
import React from "react";
import {createStyles, Typography, withStyles, WithStyles} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";

const functionsImage = require('../../../../assets/azure-functions.png');

interface Props extends WithStyles<typeof styles> {

}

function WhatIsAzureFunctionsSlideComponent({classes}: Props) {
    return (
      <div className={classes.root}>
          <SlideTitle title={'What is Azure Functions?'} />
          <div className={classes.content}>
              <div>
                <ul>
                    <li><Typography variant={'h4'}>Serverless</Typography></li>
                    <li><Typography variant={'h4'}>AWS Lambda Differences</Typography></li>
                </ul>
              </div>
              <div>
                <img src={functionsImage} />
              </div>
          </div>
      </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: '48px auto',
    },
    content: {
        gridTemplateColumns: 'auto auto'
    }
});

export const WhatIsAzureFunctionsSlide = withStyles(styles)(WhatIsAzureFunctionsSlideComponent);

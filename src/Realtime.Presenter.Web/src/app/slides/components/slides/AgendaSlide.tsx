import React from "react";
import {createStyles, Typography, withStyles, WithStyles} from "@material-ui/core";
import {SlideTitle} from "./common/SlideTitle";

interface Props extends WithStyles<typeof styles> {

}

function AgendaSlideComponent({ classes }: Props) {
    return (
      <div className={classes.root}>
          <SlideTitle title={'Agenda'}/>
        <div className={classes.list}>
            <ul>
                <li><Typography variant={'h4'}>What is Azure Functions?</Typography></li>
                <li><Typography variant={'h4'}>Azure Functions v1 vs v2</Typography></li>
                <li><Typography variant={'h4'}>What is Azure SignalR?</Typography></li>
                <li><Typography variant={'h4'}>Code</Typography></li>
                <li><Typography variant={'h4'}>Questions?</Typography></li>
                <li><Typography variant={'h4'}>Thank you</Typography></li>
            </ul>
        </div>
      </div>
    );
}

const styles = createStyles({
    root: {
        gridTemplateRows: '48px auto auto'
    },
    title: {

    },
    list: {

    }
});

export const AgendaSlide = withStyles(styles)(AgendaSlideComponent);

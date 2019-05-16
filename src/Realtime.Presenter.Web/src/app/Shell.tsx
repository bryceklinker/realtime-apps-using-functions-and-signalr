import {createStyles, Paper, Theme, WithStyles, withStyles} from "@material-ui/core";
import {AppHeader} from "./header/components/AppHeader";
import {Route, Switch} from "react-router";
import {SlidePresenterContainer} from "./slides/containers/SlidePresenterContainer";
import React from "react";

interface Props extends WithStyles<typeof styles> {

}

function ShellComponent({ classes }: Props) {
    return (
        <div className={classes.root}>
            <AppHeader/>
            <Paper className={classes.content}>
                <div className={classes.slideContent}>
                    <Switch>
                        <Route path={'/'} component={SlidePresenterContainer} />
                    </Switch>
                </div>
            </Paper>
        </div>
    );
}

const styles = (theme: Theme) => createStyles({
    root: {
        height: '100%',
        gridTemplateRows: '64px auto'
    },
    content: {
        ...theme.mixins.gutters(),
        paddingTop: theme.spacing.unit * 2,
        paddingBottom: theme.spacing.unit * 2,
        backgroundColor: '#eeeeee'
    },
    slideContent: {
        boxShadow: '0px 1px 3px 0px rgba(0,0,0,0.2), 0px 1px 1px 0px rgba(0,0,0,0.14), 0px 2px 1px -1px rgba(0,0,0,0.12)',
        padding: theme.spacing.unit,
        backgroundColor: '#ffffff'
    }
});

export const Shell = withStyles(styles)(ShellComponent);

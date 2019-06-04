import React from "react";
import {
    AppBar,
    createStyles, Icon,
    IconButton,
    Toolbar,
    Typography,
    WithStyles,
    withStyles
} from "@material-ui/core";


interface Props extends WithStyles<typeof styles> {
    onOpenSettings: () => void;
}

function AppHeaderComponent({classes, onOpenSettings}: Props) {
    return (
        <AppBar position={'static'} className={classes.appbar}>
            <Toolbar className={classes.toolbar}>
                <div>
                    <Typography variant="h3" gutterBottom className={classes.title}>
                        Building Realtime Apps with Azure Functions and SignalR
                    </Typography>
                </div>
                <div>
                    <IconButton data-testid={'open-settings'} onClick={onOpenSettings}>
                        <Icon>settings</Icon>
                    </IconButton>
                </div>
            </Toolbar>
        </AppBar>
    );
}

const styles = createStyles({
    appbar: {
        display: 'grid'
    },
    toolbar: {
        display: 'grid',
        gridTemplateColumns: 'auto 48px'
    },
    title: {
        margin: 0,
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
        overflow: 'hidden'
    }
});

export const AppHeader = withStyles(styles)(AppHeaderComponent);

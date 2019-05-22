import React, {useState} from 'react';
import {
    withStyles,
    WithStyles,
    createStyles,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions, TextField, Button
} from '@material-ui/core';
import {SettingsModel} from "../models";

interface Props extends WithStyles<typeof styles> {
    isOpen: boolean;
    settings: SettingsModel,
    onSettingsUpdated: (settings: SettingsModel) => void;
    onSettingsClosed: () => void;
}

function SettingsModalComponent({classes, settings, isOpen, onSettingsUpdated, onSettingsClosed}: Props) {
    const [baseUrl, setBaseUrl] = useState(() => settings.baseUrl);
    return (
        <Dialog open={isOpen}>
            <DialogTitle data-testid={'settings-modal'}>Settings</DialogTitle>
            <DialogContent>
                <TextField label={'Endpoint'} autoFocus value={baseUrl}
                           inputProps={{'data-testid': 'settings-base-url'}}
                           onChange={({target: {value}}) => setBaseUrl(value)}/>
            </DialogContent>
            <DialogActions>
                <Button data-testid={'save-settings'} color={'primary'} onClick={() => onSettingsUpdated({ baseUrl })}>
                    Save
                </Button>
                <Button data-testid={'cancel-settings'} color={'primary'} onClick={onSettingsClosed}>
                    Cancel
                </Button>
            </DialogActions>
        </Dialog>
    );
}

const styles = createStyles({});

export const SettingsModal = withStyles(styles)(SettingsModalComponent);

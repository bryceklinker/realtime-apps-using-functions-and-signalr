import {createMuiTheme} from "@material-ui/core";
import blue from '@material-ui/core/colors/blue';
import yellow from '@material-ui/core/colors/yellow';

export function createTheme() {
    return createMuiTheme({
        palette: {
            primary: blue,
            secondary: yellow
        }
    });
}

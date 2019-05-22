import {settingsReducer} from "./settings-reducer";
import {closeSettings, openSettings, settingsUpdated} from "../actions";

describe('settingsReducer', () => {
   it('should have url for azure function', () => {
       const state = settingsReducer();
       expect(state.settings.baseUrl).toEqual('https://realtime-apps-presentation-func.azurewebsites.net');
       expect(state.isOpen).toEqual(false);
   });

   it('should have new base url', () => {
       let state = settingsReducer();
       state = settingsReducer(state, settingsUpdated({ baseUrl: 'https://other.com'}));
       expect(state.settings.baseUrl).toEqual('https://other.com');
   });

   it('should have open settings', () => {
       let state = settingsReducer();
       state = settingsReducer(state, openSettings());
       expect(state.isOpen).toEqual(true);
   });

    it('should have closed settings', () => {
        let state = settingsReducer();
        state = settingsReducer(state, openSettings());
        state = settingsReducer(state, closeSettings());
        expect(state.isOpen).toEqual(false);
    });
});

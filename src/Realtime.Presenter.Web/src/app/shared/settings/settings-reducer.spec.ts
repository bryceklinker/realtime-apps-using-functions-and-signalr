import {settingsReducer} from "./settings-reducer";
import {settingsUpdateBaseUrl} from "./actions";

describe('settingsReducer', () => {
   it('should have url for azure function', () => {
       const state = settingsReducer();
       expect(state.baseUrl).toEqual('https://realtime-apps-presentation-func.azurewebsites.net');
   });

   it('should have new base url', () => {
       let state = settingsReducer();
       state = settingsReducer(state, settingsUpdateBaseUrl('https://other.com'));
       expect(state.baseUrl).toEqual('https://other.com');
   })
});

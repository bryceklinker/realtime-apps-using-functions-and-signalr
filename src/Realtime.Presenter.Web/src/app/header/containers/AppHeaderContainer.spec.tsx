import {render, fireEvent} from "react-testing-library";
import * as React from "react";

import {configureMockStore} from "../../../testing/configure-mock-store";
import {AppHeaderContainer} from "./AppHeaderContainer";
import {openSettings} from "../../settings/actions";
import {Provider} from "react-redux";
import {MockStore} from "redux-mock-store";
import {AppState} from "../../shared/app-state";

describe('AppHeaderContainer', () => {
   it('should dispatch open settings', () => {
       const store = configureMockStore();
       const {getByTestId} = renderContainer(store);
       fireEvent.click(getByTestId('open-settings'));
       expect(store.getActions()).toContainEqual(openSettings());
   });

   function renderContainer(store: MockStore<AppState>) {
       return render(
           <Provider store={store}>
               <AppHeaderContainer />
           </Provider>
       )
   }
});

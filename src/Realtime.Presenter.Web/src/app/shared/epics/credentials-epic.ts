import {ActionsObservable, ofType, StateObservable} from "redux-observable";
import {Action} from "redux";
import {catchError, map, switchMap, withLatestFrom} from "rxjs/operators";
import {fromPromise} from "rxjs/internal-compatibility";

import {loadCredentialsFailed, loadCredentialsSuccess, SharedActionTypes} from "../actions";
import {of} from "rxjs";
import {AppState} from "../app-state";
import {selectSettingsBaseUrl} from "../settings/settings-reducer";

export function credentialsEpic(actions$: ActionsObservable<Action>, state$: StateObservable<AppState>) {
    return actions$.pipe(
        ofType(SharedActionTypes.INITIALIZE),
        withLatestFrom(state$),
        switchMap(([, state]) => getCredentials(state).pipe(
                map(result => loadCredentialsSuccess(result)),
                catchError(err => of(loadCredentialsFailed(err)))
            )
        ),
    )
}

function getCredentials(state: AppState) {
    const baseUrl = selectSettingsBaseUrl(state);
    return fromPromise(fetch(`${baseUrl}/api/credentials`)).pipe(
        switchMap(r => fromPromise(r.json()))
    );
}

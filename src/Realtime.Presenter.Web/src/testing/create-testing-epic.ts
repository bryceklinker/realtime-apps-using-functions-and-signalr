import {ActionsObservable, ofType, StateObservable, Epic} from "redux-observable";
import {Action} from "redux";
import {Observable, Subject} from "rxjs";

import {AppState} from "../app/shared/app-state";
import {createRootEpic} from "../app/shared/root-epic";
import {createState} from "./create-state";

export function createTestingEpic(epic: Epic | null = null, ...actions: Action[]): TestingEpic {
    return new TestingEpic(epic, ...actions);
}

export class TestingEpic {
    initialState: AppState;
    actions$: Subject<Action>;
    state$: Subject<AppState>;
    epic: Epic;
    output$: Observable<Action>;

    constructor(epic: Epic | null = null, ...actions: Action[]) {
        this.actions$ = new Subject<Action>();
        this.state$ = new Subject<AppState>();
        this.epic = epic || createRootEpic();
        this.initialState = createState(...actions);
        this.output$ = this.epic(
            new ActionsObservable<Action>(this.actions$),
            new StateObservable(this.state$, this.initialState),
            { }
        );
    }

    next(action: Action) {
        this.actions$.next(action);
    }

    onAction(actionType: string): Observable<Action> {
        return this.output$
            .pipe(ofType(actionType));
    }
}

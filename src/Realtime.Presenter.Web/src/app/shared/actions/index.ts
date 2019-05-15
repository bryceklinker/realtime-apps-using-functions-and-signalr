import {action} from "typesafe-actions";
import {Credentials} from "../models";

export const SharedActionTypes = {
  INITIALIZE: '[Shared] Initialize',
  LOAD_CREDENTIALS_SUCCESS: '[Shared] Load Credentials Success',
  LOAD_CREDENTIALS_FAILED: '[Shared] Load Credentials Failed',
};

export function initialize() {
    return action(SharedActionTypes.INITIALIZE);
}

export function loadCredentialsSuccess(credentials: Credentials) {
    return action(SharedActionTypes.LOAD_CREDENTIALS_SUCCESS, credentials);
}

export function loadCredentialsFailed(error: any) {
    return action(SharedActionTypes.LOAD_CREDENTIALS_FAILED, error);
}

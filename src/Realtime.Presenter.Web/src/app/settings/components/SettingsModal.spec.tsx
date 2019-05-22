import React from 'react';
import {render, waitForElement, waitForElementToBeRemoved, fireEvent} from 'react-testing-library';
import {SettingsModal} from "./SettingsModal";
import {SettingsModel} from "../models";

describe('SettingsModal', () => {
    let onSettingsUpdated: jest.Mock;
    let onSettingsClosed: jest.Mock;
    let settings: SettingsModel;

    beforeEach(() => {
        onSettingsUpdated = jest.fn();
        onSettingsClosed = jest.fn();
        settings = {
            baseUrl: 'https://data.com'
        };
    });

    it('should have an open dialog', async () => {
        const {getByTestId} = renderComponent(true);

        await waitForElement(() => getByTestId('settings-modal'));
    });

    it('should not have an open dialog', async () => {
        const {getByTestId, rerender} = renderComponent(true);

        rerender(<SettingsModal settings={settings} isOpen={false} onSettingsUpdated={onSettingsUpdated}
                                onSettingsClosed={onSettingsClosed}/>);
        await waitForElementToBeRemoved(() => getByTestId('settings-modal'));
    });

    it('should call on settings updated', () => {
        const {getByTestId} = renderComponent(true);
        fireEvent.click(getByTestId('save-settings'));
        expect(onSettingsUpdated).toHaveBeenCalled();
    });

    it('should call on settings closed when cancel clicked', () => {
        const {getByTestId} = renderComponent(true);
        fireEvent.click(getByTestId('cancel-settings'));
        expect(onSettingsClosed).toHaveBeenCalled();
    });

    it('should have updated settings value', () => {
        const {getByTestId} = renderComponent(true);

        fireEvent.change(getByTestId('settings-base-url'), {target: {value: 'https://something.com'}});
        expect(getByTestId('settings-base-url')).toHaveAttribute('value', 'https://something.com');
    });

    it('should pass new settings to settings updated', () => {
        const {getByTestId} = renderComponent(true);

        fireEvent.change(getByTestId('settings-base-url'), {target: {value: 'https://something.com'}});
        fireEvent.click(getByTestId('save-settings'));

        expect(onSettingsUpdated).toHaveBeenCalledWith({ baseUrl: 'https://something.com' });
    });

    function renderComponent(isOpen: boolean) {
        return render(<SettingsModal isOpen={isOpen} settings={settings} onSettingsUpdated={onSettingsUpdated}
                                     onSettingsClosed={onSettingsClosed}/>);
    }
});

import React, { Component } from 'react';
import {
    Layout,
    Page,
    FooterHelp,
    Card,
    Link,
    Button,
    FormLayout,
    TextField,
    AccountConnection,
    ChoiceList,
    SettingToggle,
    Checkbox
} from '@shopify/polaris';

export class WebHook extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Checkbox
                label="I'm a webhook!"
            />
        );
    }
}

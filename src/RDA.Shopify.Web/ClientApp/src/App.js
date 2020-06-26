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
} from '@shopify/polaris';
import { ImportMinor } from '@shopify/polaris-icons';
import { flowRight as compose } from 'lodash';
import { graphql } from 'react-apollo';
import gql from 'graphql-tag';
import { WebHook } from './components/WebHook'
import { useQuery } from '@apollo/react-hooks';

import './custom.css'


const query = gql`
  query query {
    shop {
      name
      description
    }
  }
`;

function App() {
    const { loading, error, data } = useQuery(query);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error :( {error}</p>;

    return (
        <div>
            <p>
                {data.shop.name} : {data.shop.description}
            </p>
        </div>
    );
}

export default App;
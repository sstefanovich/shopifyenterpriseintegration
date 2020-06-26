import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import '@shopify/polaris/styles.css';
import enTranslations from '@shopify/polaris/locales/en.json';
import { AppProvider, Page, Card, Button } from '@shopify/polaris';
import { ApolloClient } from 'apollo-client';
import { createHttpLink } from 'apollo-link-http';
import { setContext } from 'apollo-link-context';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { ApolloProvider } from 'react-apollo';


const rootElement = document.getElementById('root');

//TO-DO these should be loaded from server settings

const httpLink = createHttpLink({ uri: 'https://steverdadev.myshopify.com/api/graphql' })

const middlewareLink = setContext(() => ({
    headers: {
        'X-Shopify-Storefront-Access-Token': '4c9f5ac91372fbe75f9d9c99bb8f1a80'
    }
}))

const client = new ApolloClient({
    link: middlewareLink.concat(httpLink),
    cache: new InMemoryCache(),
})

ReactDOM.render(
    <AppProvider i18n={enTranslations}>
        <ApolloProvider client={client}>
            <App />
        </ApolloProvider>
    </AppProvider>,
  rootElement);


import React from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import Layout from "./components/Layout";
import GlobalStyles from "./styles/GlobalStyles";
import './App.css';

import { store } from './redux/store'
import { Provider } from 'react-redux'

// MSAL imports
import {MsalProvider, useIsAuthenticated, useMsal} from "@azure/msal-react";
import { IPublicClientApplication } from "@azure/msal-browser";
import { CustomNavigationClient } from "./auth/NavigationClient";
import ServerList from "./components/Server/ServerList";
import SignInSignOutButton from "./components/authentication/SignInSignOutButton";
import {LoginDisplay} from "./components/authentication/LoginDisplay/LoginDisplay";

type AppProps = {
  pca: IPublicClientApplication
};

function App({pca }: AppProps) {

    //useMsalAuthentication(InteractionType.Redirect)
  const history = useNavigate();
  const navigationClient = new CustomNavigationClient(history);
    pca.setNavigationClient(navigationClient);
  const { accounts, instance, inProgress } = useMsal();

  return (
      <>
          <MsalProvider instance={pca}>
              <Provider store={store}>
                <Pages />
              </Provider>
          </MsalProvider>
          <GlobalStyles/>
      </>
  );
}

function Pages() {
    const isAuthenticated = useIsAuthenticated();

  return (
      <>
          <AuthenticatedTemplate>
              <Layout/>
          </AuthenticatedTemplate>

          <UnauthenticatedTemplate>
              <LoginDisplay/>
          </UnauthenticatedTemplate>
    </>
  )
}

export default App;

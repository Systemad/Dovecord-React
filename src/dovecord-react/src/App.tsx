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
import styled from "styled-components";
import {DiscoverView} from "./components/Discover";

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
              <Grid>
                  <ServerList />
                  <Routes>
                      <Route path="/:id" element={
                          <>
                              <Layout/>
                          </>
                      }/>

                      <Route path="/discover" element={
                          <>
                              <DiscoverView/>
                          </>
                      }/>
                  </Routes>
              </Grid>
          </AuthenticatedTemplate>

          <UnauthenticatedTemplate>
              <LoginDisplay/>
          </UnauthenticatedTemplate>
    </>
  )
}

export default App;


export const Grid = styled.div`
    display: grid;

    grid-template-columns: 71px 240px auto 240px;
    grid-template-rows: 46px auto 52px;
    grid-template-areas:
        "SL SN CI CI"
        "SL CL CD UL"
        "SL UI CD UL";

    height: 100%;
`;
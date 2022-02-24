import React from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';

import Layout from "./components/Layout";
import GlobalStyles from "./styles/GlobalStyles";
import './App.css';

import { store } from './redux/store'
import { Provider } from 'react-redux'

// MSAL imports
import { MsalProvider, useMsal} from "@azure/msal-react";
import { IPublicClientApplication } from "@azure/msal-browser";
import { CustomNavigationClient } from "./auth/NavigationClient";

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
  return (
      <Routes>
          <Route path="/" element={
            <>
                <Layout/>
            </>
          }/>
      </Routes>
  )
}

export default App;

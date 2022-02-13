import React from 'react';
import { Routes, Route, useNavigate } from 'react-router-dom';

import Layout from "./components/Layout";
import GlobalStyles from "./styles/GlobalStyles";
import './App.css';

// MSAL imports
import { MsalProvider } from "@azure/msal-react";
import { IPublicClientApplication } from "@azure/msal-browser";
import { CustomNavigationClient } from "./auth/NavigationClient";

type AppProps = {
  pca: IPublicClientApplication
};

function App({pca }: AppProps) {

  const history = useNavigate();
  const navigationClient = new CustomNavigationClient(history);
  pca.setNavigationClient(navigationClient);

  return (
      <MsalProvider instance={pca}>
        <Pages />
      </MsalProvider>
  );
}

function Pages() {
  return (
      <Routes>
          <Route path="/" element={
            <>
              <Layout/>
              <GlobalStyles/>
            </>
          }/>
      </Routes>
  )
}

export default App;

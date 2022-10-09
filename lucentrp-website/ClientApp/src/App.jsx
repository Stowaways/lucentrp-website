import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom"

import { AccountContextProvider } from "./context/AccountContext/AccountContext";

import LandingPage from "./components/pages/landingpage/LandingPage";
import LoadingPage from "./components/pages/loadingpage/LoadingPage";
const SignupPage = React.lazy(() => import("./components/pages/signuppage/SignupPage"));
const LoginPage = React.lazy(() => import("./components/pages/loginpage/LoginPage"));
const DashboardPage = React.lazy(() => import("./components/pages/dashboardpage/DashboardPage"));

/**
 * The main application component.
 * 
 * @returns The application component.
 */
function App() {
  return (
    <AccountContextProvider>
      <BrowserRouter>
        <React.Suspense fallback={<LoadingPage />}>
          <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/signup" element={<SignupPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/dashboard" element={<DashboardPage />} />
          </Routes>
        </React.Suspense>
      </BrowserRouter>
    </AccountContextProvider>
  );
}

export default App;
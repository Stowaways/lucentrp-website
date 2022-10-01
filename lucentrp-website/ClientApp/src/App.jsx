import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom"

import LandingPage from "./components/pages/landingpage/LandingPage";
import LoadingPage from "./components/pages/loadingpage/LoadingPage";
const SignupPage = React.lazy(() => import("./components/pages/signuppage/SignupPage"));
const LoginPage = React.lazy(() => import("./components/pages/loginpage/LoginPage"));

/**
 * The main application component.
 * 
 * @returns The application component.
 */
function App() {
  return (
    <BrowserRouter>
      <React.Suspense fallback={<LoadingPage />}>
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/signup" element={<SignupPage />} />
          <Route path="/login" element={<LoginPage />} />
        </Routes>
      </React.Suspense>
    </BrowserRouter>
  );
}

export default App;
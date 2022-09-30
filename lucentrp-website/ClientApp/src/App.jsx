import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import LandingPage from './components/pages/LandingPage/LandingPage';

/**
 * The main application component.
 * 
 * @returns The application component.
 */
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<LandingPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
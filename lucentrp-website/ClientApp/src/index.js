import React from 'react';
import ReactDOM from 'react-dom/client';

import * as serviceWorkerRegistration from './serviceWorkerRegistration';

import App from './App';

import './index.css';

const root = ReactDOM.createRoot(document.getElementById('root'));

// Render the application.
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// Register a service worker.
serviceWorkerRegistration.register();
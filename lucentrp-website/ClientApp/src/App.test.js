import { render } from '@testing-library/react';

import App from './App';

describe("Main component test cases", () => {
  test("Renders the app component without crashing", () => {
    render(<App />);
  });
});

describe("Environment variable test cases", () => {
  test("REACT_APP_HAMBURGER_START_WIDTH loads", () => {
    expect(process.env.REACT_APP_HAMBURGER_START_WIDTH).toBeDefined();
  });
});

import { render } from '@testing-library/react';

import App from './App';

describe("Main component test cases", () => {
  test("Renders the app component without crashing", () => {
    render(<App />);
  });
});
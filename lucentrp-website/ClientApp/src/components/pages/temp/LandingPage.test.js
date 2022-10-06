import { render } from '@testing-library/react';

import LandingPage from './LandingPage';

describe("Landing page test cases", () => {
    test("Landing page renders without crashing", () => {
        render(<LandingPage />);
    });
});
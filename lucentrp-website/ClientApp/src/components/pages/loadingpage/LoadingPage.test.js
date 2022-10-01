import { render } from '@testing-library/react';

import LoadingPage from './LoadingPage';

describe("Loading page test cases", () => {
    test("Loading page renders without crashing", () => {
        render(<LoadingPage />);
    });
});
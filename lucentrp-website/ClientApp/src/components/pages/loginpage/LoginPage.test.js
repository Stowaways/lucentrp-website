import { render } from '@testing-library/react';

import LoginPage from './LoginPage';

describe("Login page test cases", () => {
    test("Login page renders without crashing", () => {
        render(<LoginPage />);
    });
});
